using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace Aimrank.Pod.Infrastructure.Network
{
    public static class ClusterClient
    {
        public static async Task<bool> ConnectToCluster()
        {
            using var httpClient = new HttpClient();
            
            var podAddressFactory = new PodAddressFactory(PodSettingsProvider.Settings);

            while (true)
            {
                try
                {
                    var response = await httpClient.PostAsJsonAsync(
                        $"{PodSettingsProvider.Settings.ClusterAddress}/api/cluster", new
                        {
                            IpAddress = podAddressFactory.CreateAddress(),
                            PodSettingsProvider.Settings.MaxServers
                        });

                    return response.IsSuccessStatusCode;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);

                    await Task.Delay(10000);
                }
            }
        }
    }
}