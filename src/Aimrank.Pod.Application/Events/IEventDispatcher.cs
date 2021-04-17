using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Application.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IEvent @event, CancellationToken cancellationToken = default);
    }
}