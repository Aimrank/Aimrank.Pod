using System.Linq;
using System.Net.Sockets;
using System.Net;

namespace Aimrank.Pod.Infrastructure.Network
{
    internal class PodAddressFactory
    {
        private readonly PodSettings _podSettings;

        public PodAddressFactory(PodSettings podSettings)
        {
            _podSettings = podSettings;
        }

        public string CreateAddress(int? port = null)
            =>  $"{GetIpAddress() ?? "localhost"}:{port ?? _podSettings.Port}";
        
        private string GetIpAddress() => _podSettings.HostName ?? GetLocalIpAddress();

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
        }
    }
}