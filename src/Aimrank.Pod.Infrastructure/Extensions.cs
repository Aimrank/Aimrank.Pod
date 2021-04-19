using Aimrank.Pod.Application.Contracts;
using Aimrank.Pod.Application.Events;
using Aimrank.Pod.Application.Server;
using Aimrank.Pod.Infrastructure.Application.Events;
using Aimrank.Pod.Infrastructure.Application.Server;
using Aimrank.Pod.Infrastructure.Cluster;
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
            services.Configure<PodSettings>(configuration.GetSection(nameof(PodSettings)));
            services.Configure<ServerSettings>(configuration.GetSection(nameof(ServerSettings)));
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            
            services.AddMediatR(typeof(ICommand));
            
            services.AddHttpClient();
            services.AddSingleton<IServerEventMapper, ServerEventMapper>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddSingleton<IClusterClient, ClusterClient>();
            services.AddSingleton<PodAddressFactory>();

            var serverSettings = configuration.GetSection(nameof(ServerSettings)).Get<ServerSettings>();
            if (serverSettings?.UseFakeServerProcessManager ?? false)
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