using System;

namespace Aimrank.CSGO.Api.Contracts
{
    public class ProcessServerEventRequest
    {
        public Guid MatchId { get; set; }
        public string Name { get; set; }
        public dynamic Data { get; set; }
    }
}