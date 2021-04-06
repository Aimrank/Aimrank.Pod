using Aimrank.CSGO.Application.Contracts;
using System;

namespace Aimrank.CSGO.Application.Server.Commands.PlayerDisconnected
{
    public class PlayerDisconnectCommand : ICommand
    {
        public Guid MatchId { get; }
        public string SteamId { get; }

        public PlayerDisconnectCommand(Guid matchId, string steamId)
        {
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}