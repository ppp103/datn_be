﻿using datn.Application;
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
