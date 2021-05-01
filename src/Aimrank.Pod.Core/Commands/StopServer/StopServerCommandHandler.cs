using Aimrank.Pod.Core.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Core.Commands.StopServer
{
    internal class StopServerCommandHandler : IRequestHandler<StopServerCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;

        public StopServerCommandHandler(IServerProcessManager serverProcessManager)
        {
            _serverProcessManager = serverProcessManager;
        }

        public Task<Unit> Handle(StopServerCommand request, CancellationToken cancellationToken)
        {
            _serverProcessManager.StopServer(request.MatchId);

            return Task.FromResult(Unit.Value);
        }
    }
}