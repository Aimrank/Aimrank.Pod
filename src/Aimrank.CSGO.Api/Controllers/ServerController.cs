using Aimrank.CSGO.Application.Server.StartServer;
using Aimrank.CSGO.Application.Server.StopServer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Aimrank.CSGO.Api.Controllers
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
            
            return Ok(new {Address = address});
        }

        [HttpDelete("{matchId}")]
        public async Task<IActionResult> StopServer(Guid matchId)
        {
            await _mediator.Send(new StopServerCommand(matchId));
            
            return Ok();
        }
    }
}