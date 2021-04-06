using System;

namespace Aimrank.CSGO.Infrastructure.Application.Server.Exceptions
{
    internal class ServerReservationException : Exception
    {
        public ServerReservationException() : base("Could not create server reservation")
        {
        }
    }
}