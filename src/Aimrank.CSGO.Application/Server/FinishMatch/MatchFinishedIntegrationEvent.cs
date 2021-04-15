using Aimrank.CSGO.Application.Events;
using System;

namespace Aimrank.CSGO.Application.Server.FinishMatch
{
    public class MatchFinishedIntegrationEvent : IIntegrationEvent
    {
        public Guid MatchId { get; }
        public int Winner { get; }
        public FinishMatchCommand.MatchEndEventTeam TeamTerrorists { get; }
        public FinishMatchCommand.MatchEndEventTeam TeamCounterTerrorists { get; }

        public MatchFinishedIntegrationEvent(
            Guid matchId,
            int winner, 
            FinishMatchCommand.MatchEndEventTeam teamTerrorists,
            FinishMatchCommand.MatchEndEventTeam teamCounterTerrorists)
        {
            MatchId = matchId;
            Winner = winner;
            TeamTerrorists = teamTerrorists;
            TeamCounterTerrorists = teamCounterTerrorists;
        }
    }
}