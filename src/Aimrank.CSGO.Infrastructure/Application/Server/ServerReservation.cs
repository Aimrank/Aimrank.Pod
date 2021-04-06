using System;

namespace Aimrank.CSGO.Infrastructure.Application.Server
{
    internal record ServerReservation(Guid MatchId, string SteamKey, int Port);
}