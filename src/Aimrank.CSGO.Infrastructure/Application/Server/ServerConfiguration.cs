using System.Collections.Generic;

namespace Aimrank.CSGO.Infrastructure.Application.Server
{
    internal record ServerConfiguration(string SteamKey, int Port, IEnumerable<string> Whitelist, string Map);
}