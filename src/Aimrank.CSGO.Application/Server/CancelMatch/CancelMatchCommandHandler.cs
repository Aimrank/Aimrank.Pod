using Aimrank.CSGO.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.CSGO.Application.Server.CancelMatch
{
    internal class CancelMatchCommandHandler : ICommandHandler<CancelMatchCommand>
    {
        public Task<Unit> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Stop cs go server and free resources");
            
            return Task.FromResult(Unit.Value);
        }
    }
}