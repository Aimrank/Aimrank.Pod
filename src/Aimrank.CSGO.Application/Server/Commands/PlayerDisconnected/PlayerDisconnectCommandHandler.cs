using Aimrank.CSGO.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.CSGO.Application.Server.Commands.PlayerDisconnected
{
    internal class PlayerDisconnectCommandHandler : ICommandHandler<PlayerDisconnectCommand>
    {
        public Task<Unit> Handle(PlayerDisconnectCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Player left");
            
            return Task.FromResult(Unit.Value);
        }
    }
}