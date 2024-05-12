using datn.Domain;
using datn.Domain.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updateUserEntity = new User()
            {
                Id = request.Id,
                UserName = request.UserName,
                Email = request.Email,
                ImgLink = request.ImgLink,
            };

            return await _userRepository.UpdateUserAsync(request.Id, updateUserEntity);
        }
    }
}
