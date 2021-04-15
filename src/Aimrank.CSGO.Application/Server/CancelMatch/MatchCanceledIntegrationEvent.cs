using Aimrank.CSGO.Application.Events;
using System;

namespace Aimrank.CSGO.Application.Server.CancelMatch
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