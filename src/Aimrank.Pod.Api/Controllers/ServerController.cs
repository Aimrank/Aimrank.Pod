using Aimrank.Pod.Application.Server.StartServer;
using Aimrank.Pod.Application.Server.StopServer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Aimrank.Pod.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> StartServer(StartServerCommand command)
        {
            var address = await _mediator.Send(command);
            
            Response.Headers.Add("x-resource", address);
            
            return Ok();
        }

        [HttpDelete("{matchId}")]
        public async Task<IActionResult> StopServer(Guid matchId)
        {
            await _mediator.Send(new StopServerCommand(matchId));
            
            return Ok();
        }
    }
}