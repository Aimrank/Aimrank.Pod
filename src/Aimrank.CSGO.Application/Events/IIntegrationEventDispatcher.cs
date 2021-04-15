using System.Threading;
using System.Threading.Tasks;

namespace Aimrank.CSGO.Application.Events
{
    public interface IIntegrationEventDispatcher
    {
        Task DispatchAsync(IIntegrationEvent @event, CancellationToken cancellationToken = default);
    }
}