using System;

namespace Aimrank.Pod.Infrastructure.CSGO
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }
}