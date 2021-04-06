using System;

namespace Aimrank.CSGO.Infrastructure.Application.Server.Exceptions
{
    internal class ServerProcessStartException : Exception
    {
        public ServerProcessStartException() : base("Could not start CS:GO server")
        {
        }
    }
}