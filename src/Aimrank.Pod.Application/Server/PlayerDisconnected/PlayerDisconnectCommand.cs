using Aimrank.Pod.Application.Contracts;
using System;

namespace Aimrank.Pod.Application.Server.PlayerDisconnected
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