using Microsoft.AspNetCore.Mvc;

namespace Aimrank.CSGO.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => Ok("Aimrank.CSGO");
    }
}