using Aimrank.CSGO.Application.Server;
using Aimrank.CSGO.Infrastructure.Application.Server;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.CSGO.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ServerSettings serverSettings)
        {
            services.AddSingleton(serverSettings);
            services.AddSingleton<IServerEventMapper, ServerEventMapper>();

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