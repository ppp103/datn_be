using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        public UpdatePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UpdatePasswordResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {

            return _userRepository.UpdatePassword(new UpdatePasswordDto()
            {
                Id = request.Id,
                OldPassword = request.OldPassword,
                NewPassword = request.NewPassword,
            });
        }
    }
}
