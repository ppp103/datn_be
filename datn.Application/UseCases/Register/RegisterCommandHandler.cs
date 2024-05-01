using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegistrationResponse>
    {
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IUserRepository userRepository)
        {

            _userRepository = userRepository;   

        }
        public Task<RegistrationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = new RegisterUserDto()
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                Role = (int)Role.User, // 0: user / 1: admin
            };

            return _userRepository.ResgisterUserAsync(registerDto);
        }
    }
}
