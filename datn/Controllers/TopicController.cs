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
        public async Task<IActionResult> GetAllPaggingAsync([FromQuery] GetTopicTreeQuery request)
        {
            var topics = await Mediator.Send(request);
            return Ok(topics);
        }

        [HttpGet]
        [Route("get-flat-topic")]
        public async Task<IActionResult> GetFlatAllAsync([FromQuery] int parentId)
        {
            var topics = await Mediator.Send(new GetTopicFlatQuery{ ParentId = parentId });
            return Ok(topics);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateTopicCommand command)
        {
            var createdTopic= await Mediator.Send(command);

            if (createdTopic != null)
            {
                return StatusCode(201, createdTopic);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateTopicCommand command)
        {
            var updatedTopic = await Mediator.Send(command);

            return StatusCode(200, updatedTopic);
        }
    }
}
