using System;

namespace Aimrank.Pod.Infrastructure.Application.Server
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }
}