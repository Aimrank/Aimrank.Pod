using Aimrank.CSGO.Application.Contracts;
using Aimrank.CSGO.Application.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.CSGO.Application.Server.CancelMatch
{
    internal class CancelMatchCommandHandler : ICommandHandler<CancelMatchCommand>
    {
        private readonly IIntegrationEventDispatcher _dispatcher;
        private readonly IServerProcessManager _serverProcessManager;

        public CancelMatchCommandHandler(
            IIntegrationEventDispatcher dispatcher,
            IServerProcessManager serverProcessManager)
        {
            _dispatcher = dispatcher;
            _serverProcessManager = serverProcessManager;
        }

        public async Task<Unit> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            _serverProcessManager.StopServer(request.MatchId);
            
            await _dispatcher.DispatchAsync(new MatchCanceledIntegrationEvent(request.MatchId), cancellationToken);
            
            return Unit.Value;
        }
    }
}