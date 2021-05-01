using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Pod.Infrastructure.Cluster
{
    internal static class Extensions
    {
        public static IServiceCollection AddClusterClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PodSettings>(configuration.GetSection(nameof(PodSettings)));
            
            services.AddHttpClient();
            services.AddSingleton<IClusterClient, ClusterClient>();
            services.AddSingleton<PodAddressFactory>();
            
            return services;
        }
    }
}