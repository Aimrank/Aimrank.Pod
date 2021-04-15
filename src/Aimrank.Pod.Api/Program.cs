using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace Aimrank.Pod.Api
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await ConnectToCluster();
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static async Task ConnectToCluster()
        {
            using var httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync(
                $"{PodSettingsProvider.Settings.ClusterAddress}/api/cluster", new
                {
                    IpAddress = "localhost",
                    PodSettingsProvider.Settings.MaxServers
                });

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not connect to cluster.");
            }
        }
    }
}