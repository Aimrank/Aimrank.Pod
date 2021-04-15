using Aimrank.Pod.Application.Events;
using System;

namespace Aimrank.Pod.Application.Server.StartMatch
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