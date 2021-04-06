using Aimrank.CSGO.Application.Contracts;
using System;

namespace Aimrank.CSGO.Application.Server
{
    public interface IServerEventMapper
    {
        ICommand Map(Guid matchId, string name, dynamic data);
    }
}