using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, UpdatePasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        public UpdateEmailCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UpdatePasswordResponse> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var updateEmailDto = new UpdateEmailDto()
            {
                Id = request.Id,
                Password = request.Password,
                Email = request.Email
            };
            return await _userRepository.UpdateEmailAsync(updateEmailDto);
        }
    }
}
