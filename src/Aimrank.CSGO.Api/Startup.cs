using Aimrank.CSGO.Infrastructure.Application.Server;
using Aimrank.CSGO.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aimrank.CSGO.Api
{
    public class Startup
    {
        private readonly ServerSettings _serverSettings = new();

        public Startup(IConfiguration configuration)
        {
            configuration.GetSection(nameof(ServerSettings)).Bind(_serverSettings);
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddInfrastructure(_serverSettings);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Aimrank.CSGO"));
            });
        }
    }
}