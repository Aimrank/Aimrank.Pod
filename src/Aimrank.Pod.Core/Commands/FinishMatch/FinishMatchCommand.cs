using MediatR;
using System.Collections.Generic;
using System;

namespace Aimrank.Pod.Core.Commands.FinishMatch
{
    public class FinishMatchCommand : IRequest
    {
        public Guid MatchId { get; }
        public int Winner { get; }
        public MatchEndEventTeam TeamTerrorists { get; }
        public MatchEndEventTeam TeamCounterTerrorists { get; }

        public FinishMatchCommand(
            Guid matchId,
            int winner,
            MatchEndEventTeam teamTerrorists,
            MatchEndEventTeam teamCounterTerrorists)
        {
            MatchId = matchId;
            Winner = winner;
            TeamTerrorists = teamTerrorists;
            TeamCounterTerrorists = teamCounterTerrorists;
        }

        public record MatchEndEventTeam(int Score, IEnumerable<MatchEndEventPlayer> Clients);
        public record MatchEndEventPlayer(string SteamId, string Name, int Kills, int Assists, int Deaths, int Hs);
    }
}