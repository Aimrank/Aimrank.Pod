using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Pod.Api.Middleware
{
    public static class ExceptionsHandlerExtensions
    {
        public static IServiceCollection AddExceptionsHandler(this IServiceCollection services)
        {
            services.AddScoped<ExceptionsHandlerMiddleware>();
            return services;
        }
        
        public static IApplicationBuilder UseExceptionsHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionsHandlerMiddleware>();
            return app;
        }
    }
}