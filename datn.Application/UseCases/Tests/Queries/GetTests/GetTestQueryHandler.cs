using AutoMapper;
using datn.Domain;
using MediatR;

namespace datn.Application
{
    public class GetTestQueryHandler : IRequestHandler<GetTestQuery, PagedList<TestDto>>
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public GetTestQueryHandler(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<TestDto>> Handle(GetTestQuery request, CancellationToken cancellationToken)
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
            var testList = await _testRepository.GetAllTestPaggingAsync(
                request.PageNumber,
                request.PageSize,
                request.Keyword);
            //var questionList = await _questionRepository.GetAllQuestionPaggingAsync(request);
            //var res = _mapper.Map<Question>(questionList);
            return testList;
        }
    }
}
