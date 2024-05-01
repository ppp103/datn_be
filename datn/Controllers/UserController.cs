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
    }
}
