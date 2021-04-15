using Aimrank.CSGO.Application.Contracts;
using Aimrank.CSGO.Application.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.CSGO.Application.Server.FinishMatch
{
    internal class FinishMatchCommandHandler : ICommandHandler<FinishMatchCommand>
    {
        private readonly IIntegrationEventDispatcher _dispatcher;
        private readonly IServerProcessManager _serverProcessManager;

        public FinishMatchCommandHandler(
            IIntegrationEventDispatcher dispatcher,
            IServerProcessManager serverProcessManager)
        {
            _dispatcher = dispatcher;
            _serverProcessManager = serverProcessManager;
        }

        public async Task<Unit> Handle(FinishMatchCommand request, CancellationToken cancellationToken)
        {
            _serverProcessManager.StopServer(request.MatchId);

            await _dispatcher.DispatchAsync(new MatchFinishedIntegrationEvent(
                request.MatchId,
                request.Winner,
                request.TeamTerrorists,
                request.TeamCounterTerrorists), cancellationToken);
            
            return Unit.Value;
        }
    }
}