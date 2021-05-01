using Aimrank.Pod.Core.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Pod.Infrastructure.EventBus
{
    internal static class Extensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            services.AddSingleton<IEventsDispatcher, RabbitMQEventsDispatcher>();
            
            return services;
        }
    }
}