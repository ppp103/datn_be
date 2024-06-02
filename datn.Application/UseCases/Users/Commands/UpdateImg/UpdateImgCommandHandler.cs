using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application.UseCases.Users.Commands.UpdateImg
{
    public class UpdateImgCommandHandler : IRequestHandler<UpdateImgCommand, UpdatePasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        public UpdateImgCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UpdatePasswordResponse> Handle(UpdateImgCommand request, CancellationToken cancellationToken)
        {
            var imgLink = "";
            if (request.File != null)
            {
                string image = request.File.FileName;
                imgLink = await UploadFileHelper.UploadFile(request.File, @"user", image.ToLower());
            }

            var updateImgDto = new UpdateImgDto()
            {
                Id = request.Id,
                ImgLink = imgLink,
            };

            return await _userRepository.UpdateImgAsync(updateImgDto);
        }
    }
}
