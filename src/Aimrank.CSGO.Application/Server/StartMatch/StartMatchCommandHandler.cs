using Aimrank.CSGO.Application.Contracts;
using Aimrank.CSGO.Application.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.CSGO.Application.Server.StartMatch
{
    internal class StartMatchCommandHandler : ICommandHandler<StartMatchCommand>
    {
        private readonly IIntegrationEventDispatcher _dispatcher;

        public StartMatchCommandHandler(IIntegrationEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<Unit> Handle(StartMatchCommand request, CancellationToken cancellationToken)
        {
            await _dispatcher.DispatchAsync(new MatchStartedIntegrationEvent(request.MatchId), cancellationToken);
            
            return Unit.Value;
        }
    }
}