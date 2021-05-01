using MediatR;
using System;

namespace Aimrank.Pod.Core.Commands.PlayerDisconnected
{
    public class PlayerDisconnectCommand : IRequest
    {
        public Guid MatchId { get; }
        public string SteamId { get; }

        public PlayerDisconnectCommand(Guid matchId, string steamId)
        {
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}