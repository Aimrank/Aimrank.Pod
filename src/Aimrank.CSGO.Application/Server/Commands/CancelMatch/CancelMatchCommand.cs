using Aimrank.CSGO.Application.Contracts;
using System;

namespace Aimrank.CSGO.Application.Server.Commands.CancelMatch
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