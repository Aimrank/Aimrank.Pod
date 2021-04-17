using Aimrank.Pod.Application.Contracts;
using Aimrank.Pod.Application.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Application.Server.CancelMatch
{
    internal class CancelMatchCommandHandler : ICommandHandler<CancelMatchCommand>
    {
        private readonly IEventDispatcher _dispatcher;
        private readonly IServerProcessManager _serverProcessManager;

        public CancelMatchCommandHandler(
            IEventDispatcher dispatcher,
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