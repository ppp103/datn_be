using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application.UseCases
{
    public class UpdatePasswordAdminCommandHandler : IRequestHandler<UpdatePasswordAdminCommand, UpdatePasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        public UpdatePasswordAdminCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UpdatePasswordResponse> Handle(UpdatePasswordAdminCommand request, CancellationToken cancellationToken)
        {
            return _userRepository.UpdatePasswordAdminAsync(new UpdatePasswordDto()
            {
                Id = request.Id,
                NewPassword = request.NewPassword,
            });
        }
    }
}
