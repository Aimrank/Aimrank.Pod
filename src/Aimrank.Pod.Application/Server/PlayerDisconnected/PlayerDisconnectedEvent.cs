using Aimrank.Pod.Application.Events;
using System;

namespace Aimrank.Pod.Application.Server.PlayerDisconnected
{
    public class PlayerDisconnectedEvent : EventBase
    {
        public Guid MatchId { get; }
        public string SteamId { get; }

        public PlayerDisconnectedEvent(Guid matchId, string steamId)
        {
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}