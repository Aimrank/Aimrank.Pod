using MediatR;
using System.Collections.Generic;
using System;

namespace Aimrank.Pod.Core.Commands.StartServer
{
    public class StartServerCommand : IRequest<string>
    {
        public Guid MatchId { get; }
        public string SteamToken { get; }
        public string Map { get; }
        public IEnumerable<string> Whitelist { get; }

        public StartServerCommand(Guid matchId, string steamToken, string map, IEnumerable<string> whitelist)
        {
            MatchId = matchId;
            SteamToken = steamToken;
            Map = map;
            Whitelist = whitelist;
        }
    }
}