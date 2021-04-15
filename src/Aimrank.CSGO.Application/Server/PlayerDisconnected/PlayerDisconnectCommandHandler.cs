using Aimrank.CSGO.Application.Contracts;
using Aimrank.CSGO.Application.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.CSGO.Application.Server.PlayerDisconnected
{
    internal class PlayerDisconnectCommandHandler : ICommandHandler<PlayerDisconnectCommand>
    {
        private readonly IIntegrationEventDispatcher _dispatcher;

        public PlayerDisconnectCommandHandler(IIntegrationEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<Unit> Handle(PlayerDisconnectCommand request, CancellationToken cancellationToken)
        {
            await _dispatcher.DispatchAsync(new PlayerDisconnectedIntegrationEvent(
                request.MatchId, request.SteamId), cancellationToken);
            
            return Unit.Value;
        }
    }
}