using Aimrank.Pod.Infrastructure.Network;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System;

namespace Aimrank.Pod.Api
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            if (await ClusterClient.ConnectToCluster() is false)
            {
                Console.WriteLine("Could not connect to Cluster");
                
                return 1;
            }
            
            await CreateHostBuilder(args).Build().RunAsync();

            return 0;
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}