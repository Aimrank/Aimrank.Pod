namespace Aimrank.Pod.Infrastructure.Cluster
{
    internal class PodSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string ClusterAddress { get; set; }
        public int ClusterConnectionRetries { get; set; }
        public int MaxServers { get; set; }
    }
}