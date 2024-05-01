using datn.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace datn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeTestController : ApiControllerBase
    {
        [HttpPost]
        [Route("get-practice-test-result")]
        public async Task<IActionResult> GetPracticeTestResult(GetPracticeTestResultQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var practiceTest = await Mediator.Send(new GetPracticeTestByIdQuery() { Id = id });
            if (practiceTest != null)
            {
                return Ok(practiceTest);

            }
            else { return BadRequest(); }
        }

        [HttpGet]
        [Route("get-practice-test-by-type-id")]
        public async Task<IActionResult> GetByTestIdAsync([FromQuery] int id, int type)
        {
            var practiceTest = await Mediator.Send(new GetPracticeTestByTypeIdQuery() { TestId = id, Type = type});
            if (practiceTest != null)
            {
                return Ok(practiceTest);

            }
            else { return BadRequest(); }
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePracticeTestCommand command)
        {
            var createdPracticeTest = await Mediator.Send(command);

            if (createdPracticeTest != null)
            {
                return StatusCode(201, createdPracticeTest);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
