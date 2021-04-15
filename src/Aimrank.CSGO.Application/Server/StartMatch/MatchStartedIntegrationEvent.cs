using Aimrank.CSGO.Application.Events;
using System;

namespace Aimrank.CSGO.Application.Server.StartMatch
{
    public class MatchStartedIntegrationEvent : IIntegrationEvent
    {
        public Guid MatchId { get; }

        public MatchStartedIntegrationEvent(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}