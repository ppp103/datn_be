using datn.Application.Service;
using datn.Application.Service.Models;
using datn.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace datn.Application
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, UpdatePasswordResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly string _mailjetEmail;
        private readonly IMailService _mailService;
        private readonly IUserRepository _userRepository;
        public ResetPasswordCommandHandler(IMailService mailService, IConfiguration configuration, IUserRepository userRepository)
        {
            _mailService = mailService;
            _configuration = configuration;
            _mailjetEmail = _configuration["SystemConfiguration:MailjetEmail"];
            _userRepository = userRepository;

        }

        public async Task<UpdatePasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            /// Tạo mật khẩu mới
            Random random = new Random();
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder result = new StringBuilder(12);

            for (int i = 0; i < 12; i++)
            {
                result.Append(validChars[random.Next(validChars.Length)]);
            }
            var newPassword = result;

            /// Gửi email
            var emailDetail = new EmailModel()
            {
                From = AppConstants.DEFAULT_EMAIL,
                ToEmail = request.Email,
                Subject = "Mật khẩu mới",
                Body = $"Mật khẩu mới của bạn là: {newPassword}",
                IsHtml = true
            };
            var res = await _mailService.SendEmail(emailDetail);
            /// Gửi thành công thì cập nhật mật khẩu
            if (res)
            {
                var newPasswordDto = new UpdatePasswordDto() { Email = request.Email,NewPassword = newPassword.ToString() };
                return await _userRepository.ResetPassword(newPasswordDto);
            }
            else{
                return new UpdatePasswordResponse(false, "Không gửi được email");
            }
        }
    }
}
