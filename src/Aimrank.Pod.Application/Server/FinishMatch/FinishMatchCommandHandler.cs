using Aimrank.Pod.Application.Contracts;
using Aimrank.Pod.Application.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Application.Server.FinishMatch
{
    internal class FinishMatchCommandHandler : ICommandHandler<FinishMatchCommand>
    {
        private readonly IEventDispatcher _dispatcher;
        private readonly IServerProcessManager _serverProcessManager;

        public FinishMatchCommandHandler(
            IEventDispatcher dispatcher,
            IServerProcessManager serverProcessManager)
        {
            _dispatcher = dispatcher;
            _serverProcessManager = serverProcessManager;
        }

        public Task<Unit> Handle(FinishMatchCommand request, CancellationToken cancellationToken)
        {
            _serverProcessManager.StopServer(request.MatchId);

            _dispatcher.Dispatch(new MatchFinishedEvent(
                request.MatchId,
                request.Winner,
                request.TeamTerrorists,
                request.TeamCounterTerrorists));

            return Task.FromResult(Unit.Value);
        }
    }
}