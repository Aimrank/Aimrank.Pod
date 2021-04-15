using Aimrank.CSGO.Application.Events;
using System;

namespace Aimrank.CSGO.Application.Server.PlayerDisconnected
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