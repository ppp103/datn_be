using datn.Application;
using datn.Application.Service;
using datn.Application.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace datn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ApiControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _mailjetEmail;
        private readonly IMailService _mailService;
        public EmailController(IMailService mailService, ILogger<EmailController> logger, IConfiguration configuration)
        {
            _mailService = mailService;
            _configuration = configuration;
            _logger = logger;
            _mailjetEmail = _configuration["SystemConfiguration:MailjetEmail"];
        }

        //[HttpPost]
        //[Route("SendEmail")]
        //public bool SendEmail(MailData mailData)
        //{
        //    return _mailService.SendMail(mailData);
        //}

        [HttpPost]
        [Route("SendEmailV2")]
        public async Task<IActionResult> SendEmailV2(EmailModel emailModel)
        {
            emailModel.IsHtml = true;
            var res = await _mailService.SendEmail(emailModel);
            if (res)
            {
                return Ok(res);
            }
            return BadRequest("Email sending failed.");
        }

    }
}
