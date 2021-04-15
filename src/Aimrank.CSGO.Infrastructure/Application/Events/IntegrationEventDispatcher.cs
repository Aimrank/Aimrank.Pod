using Aimrank.CSGO.Application.Events;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.CSGO.Infrastructure.Application.Events
{
    internal class IntegrationEventDispatcher : IIntegrationEventDispatcher, IDisposable
    {
        private readonly RabbitMQSettings _rabbitSettings;
        private readonly IBasicProperties _basicProperties;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public IntegrationEventDispatcher(IOptions<RabbitMQSettings> rabbitSettings)
        {
            _rabbitSettings = rabbitSettings.Value;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitSettings.HostName,
                Port = _rabbitSettings.Port,
                UserName = _rabbitSettings.Username,
                Password = _rabbitSettings.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_rabbitSettings.ExchangeName, "direct", true, false, null);

            _basicProperties = _channel.CreateBasicProperties();
            _basicProperties.Persistent = true;
        }

        public Task DispatchAsync(IIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            _channel.BasicPublish(_rabbitSettings.ExchangeName, GetRoutingKey(@event), _basicProperties, GetEventBody(@event));
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Dispose();
        }

        private string GetRoutingKey(IIntegrationEvent @event) => $"{_rabbitSettings.ServiceName}.{@event.GetType().Name}";

        private byte[] GetEventBody(IIntegrationEvent @event)
        {
            var text = JsonSerializer.Serialize(@event, @event.GetType());
            return Encoding.UTF8.GetBytes(text);
        }
    }
}