using Aimrank.CSGO.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.CSGO.Application.Server.StartServer
{
    public class StartServerCommand : ICommand<string>
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