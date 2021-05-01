using Aimrank.Pod.Core.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Core.Commands.StartServer
{
    internal class StartServerCommandHandler : IRequestHandler<StartServerCommand, string>
    {
        private readonly IServerProcessManager _serverProcessManager;

        public StartServerCommandHandler(IServerProcessManager serverProcessManager)
        {
            _serverProcessManager = serverProcessManager;
        }

        public Task<string> Handle(StartServerCommand request, CancellationToken cancellationToken)
        {
            var address = _serverProcessManager.StartServer(
                request.MatchId, request.SteamToken, request.Map, request.Whitelist);

            return Task.FromResult(address);
        }
    }
}