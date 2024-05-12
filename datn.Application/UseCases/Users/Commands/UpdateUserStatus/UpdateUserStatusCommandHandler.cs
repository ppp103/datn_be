using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserStatusCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateUserStatusAsync(request.Id, request.IsActive);
        }
    }
}
