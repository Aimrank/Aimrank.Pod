using Aimrank.Pod.Core.Events;
using Aimrank.Pod.Core.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Core.Commands.CancelMatch
{
    internal class CancelMatchCommandHandler : IRequestHandler<CancelMatchCommand>
    {
        private readonly IEventsDispatcher _dispatcher;
        private readonly IServerProcessManager _serverProcessManager;

        public CancelMatchCommandHandler(
            IEventsDispatcher dispatcher,
            IServerProcessManager serverProcessManager)
        {
            _dispatcher = dispatcher;
            _serverProcessManager = serverProcessManager;
        }

        public Task<Unit> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            _serverProcessManager.StopServer(request.MatchId);
            
            _dispatcher.Dispatch(new MatchCanceledEvent(request.MatchId));

            return Task.FromResult(Unit.Value);
        }
    }
}