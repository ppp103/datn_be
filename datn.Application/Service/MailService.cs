using datn.Application.Service.Models;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;

namespace datn.Application.Service
{
    public class MailService : IMailService
    {
        private readonly string _apiKey;
        private readonly string _secretKey;
        public MailService(IConfiguration configuration)
        {
            _apiKey = configuration["SystemConfiguration:MailjetApiKey"];
            _secretKey = configuration["SystemConfiguration:MailjetSecretKey"];
        }

        public async Task<string> FormatEmailTemplate(string fromEmail, string subject, string toEmail, string message)
        {
            string filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, $"wwwroot/files/email.html"));
            string template = File.ReadAllText($"{filePath}");

            template = template.Replace("{fromEmail}", fromEmail);
            template = template.Replace("{subject}", subject);
            template = template.Replace("{message}", message);

            return template;
        }

        public async Task<bool> SendEmail(EmailModel emailModel)
        {
            try
            {
                MailjetClient client = new MailjetClient(_apiKey, _secretKey);

                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource
                };

                //format email
                emailModel.Body = await FormatEmailTemplate(emailModel.From, emailModel.Subject, emailModel.ToEmail, emailModel.Body);

                var email = new TransactionalEmailBuilder()
                       .WithFrom(new SendContact(emailModel.From))
                       .WithSubject(emailModel.Subject)
                       .WithHtmlPart(emailModel.Body)
                       .WithTo(new SendContact(emailModel.ToEmail))
                       .Build();

                var response = await client.SendTransactionalEmailAsync(email);
                var message = response.Messages[0];

                bool result = message.Status.ToLower() == "success";


                return result;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
