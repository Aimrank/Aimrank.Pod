using System;

namespace Aimrank.CSGO.Infrastructure.Application.Server.Exceptions
{
    internal class ServerProcessStopException : Exception
    {
        public ServerProcessStopException() : base("Server not found")
        {
        }
    }
}