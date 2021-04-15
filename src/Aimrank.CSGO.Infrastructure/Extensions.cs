using Aimrank.CSGO.Application.Events;
using Aimrank.CSGO.Application.Server;
using Aimrank.CSGO.Infrastructure.Application.Events;
using Aimrank.CSGO.Infrastructure.Application.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.CSGO.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServerSettings>(configuration.GetSection(nameof(ServerSettings)));
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            
            services.AddSingleton<IServerEventMapper, ServerEventMapper>();
            services.AddSingleton<IIntegrationEventDispatcher, IntegrationEventDispatcher>();
            
            var serverSettings = new ServerSettings();
            configuration.GetSection(nameof(ServerSettings)).Bind(serverSettings);

            if (serverSettings.UseFakeServerProcessManager)
            {
                services.AddSingleton<IServerProcessManager, FakeServerProcessManager>();
            }
            else
            {
                services.AddSingleton<IServerProcessManager, ServerProcessManager>();
            }
            
            return services;
        }
    }
}