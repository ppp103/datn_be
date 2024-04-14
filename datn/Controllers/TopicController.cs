using datn.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace datn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPaggingAsync()
        {
            var topics = await Mediator.Send(new GetTopicTreeQuery());
            return Ok(topics);
        }
    }
}
