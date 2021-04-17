using Aimrank.Pod.Application.Events;
using System;

namespace Aimrank.Pod.Application.Server.StartMatch
{
    public class MatchStartedEvent : EventBase
    {
        public Guid MatchId { get; }

        public MatchStartedEvent(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}