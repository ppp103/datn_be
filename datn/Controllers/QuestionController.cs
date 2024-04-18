﻿using datn.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace datn.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var questions = await Mediator.Send(new GetQuestionQuery());
            return Ok(questions);
        }

        [HttpGet("Pagging")]
        public async Task<IActionResult> GetAllPaggingAsync([FromQuery] GetQuestionQuery request)
        {
            var questions = await Mediator.Send(request);
            return Ok(questions);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var question = await Mediator.Send(new GetQuestionByIdQuery() { QuestionId = id});
            if (question != null)
            {
                return Ok(question);

            }
            else { return BadRequest(); }
        }

        [HttpGet("get-questions-by-test-id/{id}")]
        public async Task<IActionResult> GetQuestionByTestIdAsync(int id)
        {
            var question = await Mediator.Send(new GetQuestionByTestIdQuery() { TestId = id });
            if (question != null)
            {
                return Ok(question);

            }
            else { return BadRequest(); }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateQuestionCommand command)
        {
            var createdQuestion = await Mediator.Send(command);

            if (createdQuestion != null)
            {
                return StatusCode(201, createdQuestion);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateQuestionCommand command)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(new { Errors = ModelState });
            
            var updatedQuestion = await Mediator.Send(command);

            return StatusCode(200, updatedQuestion);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var result = await Mediator.Send(new DeleteQuestionCommand { Id = id });
            if(result == 0)
            {
                return BadRequest();
            }
            return Ok(id);
        }
    }
}
