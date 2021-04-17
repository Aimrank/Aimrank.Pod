using Aimrank.Pod.Application.Events;
using System;

namespace Aimrank.Pod.Application.Server.StartMatch
{
    public class MatchStartedEvent : IEvent
    {
        public Guid MatchId { get; }

        public MatchStartedEvent(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}