using Aimrank.CSGO.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.CSGO.Application.Server.StopServer
{
    internal class StopServerCommandHandler : ICommandHandler<StopServerCommand>
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