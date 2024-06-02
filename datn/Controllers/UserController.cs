using datn.Application;
using datn.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace datn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginCommand command)
        {
            var res = await Mediator.Send(command);
            if (res.Flag)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> Register(RegisterCommand command)
        {
            var res = await Mediator.Send(command);
            if (res.Flag)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaggingAsync([FromQuery] GetAllUserPaggingQuery request)
        {
            var res = await Mediator.Send(request);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await Mediator.Send(new GetUserByIdQuery() { Id = id });
            if (user != null)
            {
                return Ok(user);

            }
            else { return BadRequest(); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateUserCommand command)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(new { Errors = ModelState });

            var updatedUser = await Mediator.Send(command);

            return StatusCode(200, updatedUser);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatusAsync(UpdateUserStatusCommand command)
        {
            var updatedUser = await Mediator.Send(command);

            return StatusCode(200, updatedUser);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordCommand command)
        {
            var updatedUser = await Mediator.Send(command);

            return StatusCode(200, updatedUser);
        }

        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateEmailAsync(UpdateEmailCommand command)
        {
            var updatedUser = await Mediator.Send(command);

            return StatusCode(200, updatedUser);
        }

        [HttpPut("update-avatar")]
        public async Task<IActionResult> UpdateAvatarAsync([FromForm]UpdateImgCommand command)
        {
            var updatedUser = await Mediator.Send(command);

            return StatusCode(200, updatedUser);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var result = await Mediator.Send(new DeleteQuestionCommand { Id = id });
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok(id);
        }
    }
}
