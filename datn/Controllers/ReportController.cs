using datn.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace datn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ApiControllerBase
    {
        [HttpGet]
        [Route("get-statistic-by-user")]
        public async Task<IActionResult> GetStatisticByUser([FromQuery] int userId, int time)
        {
            var report = Mediator.Send(new GetStatisticByUserQuery() { UserId = userId, Time = time });
            if (report != null)
            {
                return Ok(report);

            }
            else { return BadRequest(); }
        }
    }
}
