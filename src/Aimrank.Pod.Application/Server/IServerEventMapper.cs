using Aimrank.Pod.Application.Contracts;
using System;

namespace Aimrank.Pod.Application.Server
{
    public interface IServerEventMapper
    {
        ICommand Map(Guid matchId, string name, dynamic data);
    }
}