using datn.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace datn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ApiControllerBase
    {
        [HttpGet("Pagging")]
        public async Task<IActionResult> GetAllPaggingAsync([FromQuery] GetTestQuery request)
        {
            var tests = await Mediator.Send(request);
            return Ok(tests);
        }

    }
}
