using Aimrank.Pod.Application.Events;
using System;

namespace Aimrank.Pod.Application.Server.FinishMatch
{
    public class MatchFinishedEvent : EventBase
    {
        public Guid MatchId { get; }
        public int Winner { get; }
        public FinishMatchCommand.MatchEndEventTeam TeamTerrorists { get; }
        public FinishMatchCommand.MatchEndEventTeam TeamCounterTerrorists { get; }

        public MatchFinishedEvent(
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