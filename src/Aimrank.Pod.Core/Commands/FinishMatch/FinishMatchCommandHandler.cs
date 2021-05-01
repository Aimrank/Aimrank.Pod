using Aimrank.Pod.Core.Events;
using Aimrank.Pod.Core.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Core.Commands.FinishMatch
{
    internal class FinishMatchCommandHandler : IRequestHandler<FinishMatchCommand>
    {
        private readonly IEventsDispatcher _dispatcher;
        private readonly IServerProcessManager _serverProcessManager;

        public FinishMatchCommandHandler(
            IEventsDispatcher dispatcher,
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