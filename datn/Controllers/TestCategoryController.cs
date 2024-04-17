using datn.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace datn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCategoryController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var testCategories = await Mediator.Send(new GetTestCategoryQuery());
            return Ok(testCategories);
        }
    }
}
