using Aimrank.Pod.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Pod.Infrastructure.CSGO
{
    internal static class Extensions
    {
        public static IServiceCollection AddCSGOServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServerSettings>(configuration.GetSection(nameof(ServerSettings)));
            
            services.AddSingleton<IServerEventMapper, ServerEventMapper>();
            
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