using Aimrank.CSGO.Application.Contracts;
using System;

namespace Aimrank.CSGO.Application.Server.StopServer
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