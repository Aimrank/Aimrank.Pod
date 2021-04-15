using Aimrank.Pod.Api.Contracts;
using Aimrank.Pod.Application.Server;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aimrank.Pod.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CSGOController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IServerEventMapper _serverEventMapper;

        public CSGOController(IMediator mediator, IServerEventMapper serverEventMapper)
        {
            _mediator = mediator;
            _serverEventMapper = serverEventMapper;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessServerEvent(ProcessServerEventRequest request)
        {
            var command = _serverEventMapper.Map(request.MatchId, request.Name, request.Data);
            if (command is null)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return Accepted();
        }
    }
}