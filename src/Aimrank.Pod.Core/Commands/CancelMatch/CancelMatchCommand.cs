using MediatR;
using System;

namespace Aimrank.Pod.Core.Commands.CancelMatch
{
    public class CancelMatchCommand : IRequest
    {
        public Guid MatchId { get; }

        public CancelMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}