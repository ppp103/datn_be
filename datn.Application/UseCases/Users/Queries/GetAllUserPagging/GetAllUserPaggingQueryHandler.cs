using datn.Domain.Dto;
using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetAllUserPaggingQueryHandler : IRequestHandler<GetAllUserPaggingQuery, PagedList<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUserPaggingQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedList<UserDto>> Handle(GetAllUserPaggingQuery request, CancellationToken cancellationToken)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = AppConstants.MinPageSize;
            }
            if (request.PageSize == -1)
            {
                request.PageSize = AppConstants.MaxPageSize;
            }
            if (request.PageNumber == 0 || request.PageNumber == null)
            {
                request.PageNumber = 1;
            }
            return await _userRepository.GetAllUserPaggingAsync(request.PageNumber, request.PageSize, request.Keyword);
        }
    }
}
