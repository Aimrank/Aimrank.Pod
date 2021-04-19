using System.Threading.Tasks;

namespace Aimrank.Pod.Infrastructure.Cluster
{
    public interface IClusterClient
    {
        Task ConnectAsync();
    }
}