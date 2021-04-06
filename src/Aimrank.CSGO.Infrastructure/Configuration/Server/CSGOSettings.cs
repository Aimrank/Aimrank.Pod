using System.Collections.Generic;

namespace Aimrank.CSGO.Infrastructure.Configuration.Server
{
    public class CSGOSettings
    {
        public bool UseFakeServerProcessManager { get; set; }
        public IEnumerable<string> SteamKeys { get; set; }
    }
}