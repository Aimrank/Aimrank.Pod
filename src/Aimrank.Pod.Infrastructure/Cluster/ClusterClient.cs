using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace Aimrank.Pod.Infrastructure.Cluster
{
    internal class ClusterClient : IClusterClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PodAddressFactory _podAddressFactory;
        private readonly PodSettings _podSettings;
        private readonly ILogger<ClusterClient> _logger;

        public ClusterClient(
            IHttpClientFactory httpClientFactory,
            PodAddressFactory podAddressFactory,
            IOptions<PodSettings> podSettings,
            ILogger<ClusterClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _podAddressFactory = podAddressFactory;
            _logger = logger;
            _podSettings = podSettings.Value;
        }

        public async Task ConnectAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var podAddress = _podAddressFactory.CreateAddress();
            var clusterAddress = _podSettings.ClusterAddress;
            
            _logger.LogInformation($"Connecting \"{podAddress}\" to cluster at \"{clusterAddress}\"");

            var attempts = 0;
            
            while (attempts <= _podSettings.ClusterConnectionRetries)
            {
                if (await TryConnectAsync(httpClient, podAddress))
                {
                    _logger.LogInformation($"Connected \"{podAddress}\" with cluster at \"{clusterAddress}\".");

                    return;
                }
                
                attempts++;
                
                if (attempts <= _podSettings.ClusterConnectionRetries)
                {
                    await Task.Delay(10000);
                }
            }

            throw new Exception($"Could not connect \"{podAddress}\" to cluster at \"{clusterAddress}\" after {attempts} attempts.");
        }

        private async Task<bool> TryConnectAsync(HttpClient httpClient, string address)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(
                    $"{_podSettings.ClusterAddress}/pod", new
                    {
                        IpAddress = address,
                        _podSettings.MaxServers
                    });

                return response.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception.Message);
                
                return false;
            }
        }
    }
}