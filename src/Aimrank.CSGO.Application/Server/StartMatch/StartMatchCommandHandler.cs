using Aimrank.CSGO.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.CSGO.Application.Server.StartMatch
{
    internal class StartMatchCommandHandler : ICommandHandler<StartMatchCommand>
    {
        public Task<Unit> Handle(StartMatchCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Start cs go server");

            return Task.FromResult(Unit.Value);
        }
    }
}