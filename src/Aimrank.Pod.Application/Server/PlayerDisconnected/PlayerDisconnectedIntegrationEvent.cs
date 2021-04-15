using Aimrank.Pod.Application.Events;
using System;

namespace Aimrank.Pod.Application.Server.PlayerDisconnected
{
    public class PlayerDisconnectedIntegrationEvent : IIntegrationEvent
    {
        public Guid MatchId { get; }
        public string SteamId { get; }

        public PlayerDisconnectedIntegrationEvent(Guid matchId, string steamId)
        {
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}