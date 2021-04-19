using Microsoft.Extensions.Options;
using System.Linq;
using System.Net.Sockets;
using System.Net;

namespace Aimrank.Pod.Infrastructure.Cluster
{
    internal class PodAddressFactory
    {
        private readonly PodSettings _podSettings;

        public PodAddressFactory(IOptions<PodSettings> podSettings)
        {
            _podSettings = podSettings.Value;
        }

        public string CreateAddress(int? port = null)
            => $"{GetIpAddress() ?? "localhost"}:{port ?? _podSettings.Port}";

        private string GetIpAddress()
            => string.IsNullOrEmpty(_podSettings.HostName) ? GetLocalIpAddress() : _podSettings.HostName;

        private static string GetLocalIpAddress()
            => Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
    }
}