using datn.Application.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application.Service
{
    public interface IMailService
    {
        public Task<bool> SendEmail(EmailModel emailModel);

        public Task<string> FormatEmailTemplate(string fromEmail, string subject, string toEmail, string message);

    }
}
