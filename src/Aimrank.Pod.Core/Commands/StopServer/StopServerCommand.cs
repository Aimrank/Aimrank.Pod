using MediatR;
using System;

namespace Aimrank.Pod.Core.Commands.StopServer
{
    public class StopServerCommand : IRequest
    {
        public Guid MatchId { get; }

        public StopServerCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}