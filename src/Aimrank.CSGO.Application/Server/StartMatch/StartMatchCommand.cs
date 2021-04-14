using Aimrank.CSGO.Application.Contracts;
using System;

namespace Aimrank.CSGO.Application.Server.StartMatch
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