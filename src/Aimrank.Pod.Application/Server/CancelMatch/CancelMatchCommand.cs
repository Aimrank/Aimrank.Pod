using Aimrank.Pod.Application.Contracts;
using System;

namespace Aimrank.Pod.Application.Server.CancelMatch
{
    public class CancelMatchCommand : ICommand
    {
        public Guid MatchId { get; }

        public CancelMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}