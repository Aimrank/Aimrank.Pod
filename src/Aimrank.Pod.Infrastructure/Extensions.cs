using Aimrank.Pod.Application.Contracts;
using Aimrank.Pod.Application.Events;
using Aimrank.Pod.Application.Server;
using Aimrank.Pod.Infrastructure.Application.Events;
using Aimrank.Pod.Infrastructure.Application.Server;
using Aimrank.Pod.Infrastructure.Network;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Aimrank.Pod.UnitTests")]

namespace Aimrank.Pod.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServerSettings>(configuration.GetSection(nameof(ServerSettings)));
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            
            services.AddMediatR(typeof(ICommand));
            
            services.AddSingleton<IServerEventMapper, ServerEventMapper>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();

            var serverSettings = configuration.GetSection(nameof(ServerSettings)).Get<ServerSettings>();
            if (serverSettings?.UseFakeServerProcessManager ?? false)
            {
                services.AddSingleton<IServerProcessManager, FakeServerProcessManager>();
            }
            else
            {
                services.AddSingleton<IServerProcessManager, ServerProcessManager>();
            }

            var podSettings = configuration.GetSection(nameof(PodSettings)).Get<PodSettings>();

            services.AddSingleton(podSettings);
            services.AddSingleton<PodAddressFactory>();
            
            return services;
        }
    }
}