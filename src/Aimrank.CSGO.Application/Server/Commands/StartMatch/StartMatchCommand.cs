using Aimrank.CSGO.Application.Contracts;
using System;

namespace Aimrank.CSGO.Application.Server.Commands.StartMatch
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