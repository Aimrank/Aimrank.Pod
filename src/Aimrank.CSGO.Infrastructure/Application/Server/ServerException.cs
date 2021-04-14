using System;

namespace Aimrank.CSGO.Infrastructure.Application.Server
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }
}