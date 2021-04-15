using Aimrank.Pod.Application.Contracts;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Pod.Application.Server.StartServer
{
    internal class StartServerCommandHandler : ICommandHandler<StartServerCommand, string>
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