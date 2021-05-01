using MediatR;
using System;

namespace Aimrank.Pod.Core.Commands.StartMatch
{
    public class StartMatchCommand : IRequest
    {
        public Guid MatchId { get; }

        public StartMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}