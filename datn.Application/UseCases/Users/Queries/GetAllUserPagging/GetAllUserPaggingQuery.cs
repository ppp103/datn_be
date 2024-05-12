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
    public class GetAllUserPaggingQuery : PagingRequest, IRequest<PagedList<UserDto>>
    {
        
    }
}
