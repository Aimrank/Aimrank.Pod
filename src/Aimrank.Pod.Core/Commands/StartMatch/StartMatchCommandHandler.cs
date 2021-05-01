using Aimrank.Pod.Core.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Core.Commands.StartMatch
{
    internal class StartMatchCommandHandler : IRequestHandler<StartMatchCommand>
    {
        private readonly IEventsDispatcher _dispatcher;

        public StartMatchCommandHandler(IEventsDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<Unit> Handle(StartMatchCommand request, CancellationToken cancellationToken)
        {
            _dispatcher.Dispatch(new MatchStartedEvent(request.MatchId));
            
            return Task.FromResult(Unit.Value);
        }
    }
}