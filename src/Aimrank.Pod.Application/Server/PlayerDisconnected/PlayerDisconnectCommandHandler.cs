using Aimrank.Pod.Application.Contracts;
using Aimrank.Pod.Application.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Application.Server.PlayerDisconnected
{
    internal class PlayerDisconnectCommandHandler : ICommandHandler<PlayerDisconnectCommand>
    {
        private readonly IEventDispatcher _dispatcher;

        public PlayerDisconnectCommandHandler(IEventDispatcher dispatcher)
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