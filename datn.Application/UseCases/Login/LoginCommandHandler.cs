using datn.Domain;
using datn.Domain.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application.UseCases.Login
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        public LoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userDto = new LoginUserDto()
            {
                UserName = request.UserName, Password = request.Password  
            };

            return await _userRepository.LoginUserAsync(userDto);
        }
    }
}
