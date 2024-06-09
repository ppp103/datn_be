using datn.Application;
using datn.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetAllAsync([FromQuery] GetQuestionQuery request)
        {
            var questions = await Mediator.Send(request);
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

        [HttpGet("excel")]
        public async Task<IActionResult> GetExcelFile([FromQuery] GetQuestionExcelFileQuery request)
        {
            //var excelPackage = await _fixedAssetService.ExportFilteredAssetsToExcelAsync(assetFilter);

            var excelPackage = await Mediator.Send(request);

            if (excelPackage == null)
            {
                // Xử lý khi không có dữ liệu để xuất Excel
                throw new NotFoundException("Xuất excel thất bại");
            }
            using (var memoryStream = new MemoryStream())
            {
                excelPackage.SaveAs(memoryStream);
                memoryStream.Position = 0;

                var excelBytes = memoryStream.ToArray();

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_cau_hoi.xlsx");
            }
        }
    }
}
