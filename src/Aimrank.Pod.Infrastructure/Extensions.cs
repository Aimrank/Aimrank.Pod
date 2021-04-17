using System.Runtime.CompilerServices;
using Aimrank.Pod.Application.Events;
using Aimrank.Pod.Application.Server;
using Aimrank.Pod.Infrastructure.Application.Events;
using Aimrank.Pod.Infrastructure.Application.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Aimrank.Pod.UnitTests")]

namespace Aimrank.Pod.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServerSettings>(configuration.GetSection(nameof(ServerSettings)));
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            
            services.AddSingleton<IServerEventMapper, ServerEventMapper>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();

            var serverSettings = configuration.GetSection(nameof(ServerSettings)).Get<ServerSettings>();

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