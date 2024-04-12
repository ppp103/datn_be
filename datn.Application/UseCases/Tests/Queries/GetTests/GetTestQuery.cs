using datn.Domain;
using MediatR;

namespace datn.Application
{
    public class GetTestQuery : PagingRequest, IRequest<PagedList<TestDto>>
    {

    }
}
