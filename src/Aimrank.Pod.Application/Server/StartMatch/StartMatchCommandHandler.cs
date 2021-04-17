using Aimrank.Pod.Application.Contracts;
using Aimrank.Pod.Application.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Application.Server.StartMatch
{
    internal class StartMatchCommandHandler : ICommandHandler<StartMatchCommand>
    {
        private readonly IEventDispatcher _dispatcher;

        public StartMatchCommandHandler(IEventDispatcher dispatcher)
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