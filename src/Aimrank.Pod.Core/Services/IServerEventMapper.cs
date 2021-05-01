using MediatR;
using System;

namespace Aimrank.Pod.Core.Services
{
    public interface IServerEventMapper
    {
        IRequest Map(Guid matchId, string name, dynamic data);
    }
}