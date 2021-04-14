using Aimrank.CSGO.Application.Contracts;
using System;

namespace Aimrank.CSGO.Application.Server.CancelMatch
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