using datn.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace datn.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Question : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var questions = await Mediator.Send(new GetQuestionQuery());
            return Ok(questions);
        }


    }
}
