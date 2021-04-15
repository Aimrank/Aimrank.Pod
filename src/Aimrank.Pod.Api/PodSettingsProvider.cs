using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace Aimrank.Pod.Api
{
    public static class PodSettingsProvider
    {
        private static PodSettings _settings;

        public static PodSettings Settings
        {
            get
            {
                if (_settings is null)
                {
                    LoadSettings();
                }

                return _settings;
            }
        }
        
        private static void LoadSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json");

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environment is not null)
            {
                builder.AddJsonFile($"appsettings.{environment}.json", true);
            }

            builder.AddEnvironmentVariables();

            var podSettings = new PodSettings();
            builder.Build().GetSection(nameof(PodSettings)).Bind(podSettings);

            _settings = podSettings;
        }
    }
}