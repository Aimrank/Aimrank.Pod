using Aimrank.CSGO.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.CSGO.Application.Server.Commands.FinishMatch
{
    internal class FinishMatchCommandHandler : ICommandHandler<FinishMatchCommand>
    {
        public Task<Unit> Handle(FinishMatchCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Stop cs go server and free resources");
            
            return Task.FromResult(Unit.Value);
        }
    }
}