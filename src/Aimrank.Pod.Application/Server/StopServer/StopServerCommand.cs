using Aimrank.Pod.Application.Contracts;
using System;

namespace Aimrank.Pod.Application.Server.StopServer
{
    public class StopServerCommand : ICommand
    {
        public Guid MatchId { get; }

        public StopServerCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}