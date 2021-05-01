using Aimrank.Pod.Core.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Core.Commands.PlayerDisconnected
{
    internal class PlayerDisconnectCommandHandler : IRequestHandler<PlayerDisconnectCommand>
    {
        private readonly IEventsDispatcher _dispatcher;

        public PlayerDisconnectCommandHandler(IEventsDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<Unit> Handle(PlayerDisconnectCommand request, CancellationToken cancellationToken)
        {
            _dispatcher.Dispatch(new PlayerDisconnectedEvent(request.MatchId, request.SteamId));
            
            return Task.FromResult(Unit.Value);
        }
    }
}