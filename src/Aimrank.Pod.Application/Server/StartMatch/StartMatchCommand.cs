using Aimrank.Pod.Application.Contracts;
using System;

namespace Aimrank.Pod.Application.Server.StartMatch
{
    public class StartMatchCommand : ICommand
    {
        public Guid MatchId { get; }

        public StartMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}