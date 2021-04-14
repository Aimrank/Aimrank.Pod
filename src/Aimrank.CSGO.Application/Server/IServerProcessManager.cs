using System.Collections.Generic;
using System;

namespace Aimrank.CSGO.Application.Server
{
    public interface IServerProcessManager
    {
        string StartServer(Guid matchId, string steamToken, string map, IEnumerable<string> whitelist);
        void StopServer(Guid matchId);
    }
}