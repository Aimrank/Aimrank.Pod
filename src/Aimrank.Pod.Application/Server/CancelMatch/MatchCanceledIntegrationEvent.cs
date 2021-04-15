using Aimrank.Pod.Application.Events;
using System;

namespace Aimrank.Pod.Application.Server.CancelMatch
{
    public class MatchCanceledIntegrationEvent : IIntegrationEvent
    {
        public Guid MatchId { get; }

        public MatchCanceledIntegrationEvent(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}