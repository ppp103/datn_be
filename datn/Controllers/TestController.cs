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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var test = await Mediator.Send(new GetTestByIdQuery() { TestId = id });
            if (test != null)
            {
                return Ok(test);

            }
            else { return BadRequest(); }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]CreateTestCommand command)
        {
            var createdTest = await Mediator.Send(command);

            if (createdTest != null)
            {
                return StatusCode(201, createdTest);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
