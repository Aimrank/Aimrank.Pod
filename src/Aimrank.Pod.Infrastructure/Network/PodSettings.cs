namespace Aimrank.Pod.Infrastructure.Network
{
    internal class PodSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string ClusterAddress { get; set; }
        public int MaxServers { get; set; }
    }
}