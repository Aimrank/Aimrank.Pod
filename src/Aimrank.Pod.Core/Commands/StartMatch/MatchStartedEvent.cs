using Aimrank.Pod.Core.Events;
using System;

namespace Aimrank.Pod.Core.Commands.StartMatch
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