using Aimrank.Pod.Core.Events;
using Aimrank.Pod.Infrastructure.CSGO;
using Aimrank.Pod.Infrastructure.Cluster;
using Aimrank.Pod.Infrastructure.EventBus;
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
            services.AddMediatR(typeof(IEvent));
            
            services.AddEventBus(configuration);
            services.AddCSGOServer(configuration);
            services.AddClusterClient(configuration);

            return services;
        }
    }
}