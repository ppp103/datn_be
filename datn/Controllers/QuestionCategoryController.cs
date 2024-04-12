using datn.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace datn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCategoryController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var questions = await Mediator.Send(new GetQuestionCategoryQuery());
            return Ok(questions);
        }

    }
}
