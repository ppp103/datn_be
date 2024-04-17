
using datn.Domain;
using MediatR;

namespace datn.Application
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, TestDto>
    {
        private readonly ITestRepository _testRepository;
        public GetTestByIdQueryHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<TestDto> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            return await _testRepository.GetByIdAsync(request.TestId);
        }
    }
}
