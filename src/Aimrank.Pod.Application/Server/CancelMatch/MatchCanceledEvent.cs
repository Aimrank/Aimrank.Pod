using Aimrank.Pod.Application.Events;
using System;

namespace Aimrank.Pod.Application.Server.CancelMatch
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