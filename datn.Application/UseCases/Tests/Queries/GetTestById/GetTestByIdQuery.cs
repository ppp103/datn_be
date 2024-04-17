
using datn.Domain;
using MediatR;

namespace datn.Application
{
    public class GetTestByIdQuery : IRequest<TestDto>
    {
        public int TestId { get; set; }

    }
}
