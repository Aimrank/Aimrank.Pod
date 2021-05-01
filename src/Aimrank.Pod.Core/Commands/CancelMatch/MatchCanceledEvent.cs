using Aimrank.Pod.Core.Events;
using System;

namespace Aimrank.Pod.Core.Commands.CancelMatch
{
    public class MatchCanceledEvent : EventBase
    {
        public Guid MatchId { get; }

        public MatchCanceledEvent(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}