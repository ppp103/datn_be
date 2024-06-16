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
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettingsOptions, IConfiguration configuration)
        {
            _mailSettings = mailSettingsOptions.Value;
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

        public bool SendMail(MailData mailData)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    emailMessage.From.Add(emailFrom);
                    MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    emailMessage.To.Add(emailTo);

                    emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                    emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

                    emailMessage.Subject = mailData.EmailSubject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.TextBody = mailData.EmailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        mailClient.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                        mailClient.Send(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Exception Details
                return false;
            }
        }
    }
}
