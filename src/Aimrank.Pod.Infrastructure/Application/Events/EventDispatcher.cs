using Aimrank.Pod.Application.Events;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using System;

namespace Aimrank.Pod.Infrastructure.Application.Events
{
    internal class EventDispatcher : IEventDispatcher, IDisposable
    {
        private RabbitMQSettings RabbitMQSettings { get; }
        private IBasicProperties BasicProperties { get; }
        private IConnection Connection { get; }
        private IModel Channel { get; }

        public EventDispatcher(IOptions<RabbitMQSettings> rabbitSettings)
        {
            RabbitMQSettings = rabbitSettings.Value;

            var factory = new ConnectionFactory
            {
                HostName = RabbitMQSettings.HostName,
                Port = RabbitMQSettings.Port,
                UserName = RabbitMQSettings.Username,
                Password = RabbitMQSettings.Password
            };

            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(RabbitMQSettings.ExchangeName, "direct", true, false, null);

            BasicProperties = Channel.CreateBasicProperties();
            BasicProperties.Persistent = true;
        }

        public void Dispatch(IEvent @event)
            => Channel.BasicPublish(RabbitMQSettings.ExchangeName, GetRoutingKey(@event), BasicProperties, GetEventBody(@event));

        public void Dispose()
        {
            Channel?.Close();
            Connection?.Dispose();
        }

        private string GetRoutingKey(IEvent @event) => $"{RabbitMQSettings.ServiceName}.{@event.GetType().Name}";

        private byte[] GetEventBody(IEvent @event)
        {
            var text = JsonSerializer.Serialize(@event, @event.GetType());
            return Encoding.UTF8.GetBytes(text);
        }
    }
}