using AutoMapper;
using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, TestDto>
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        public async Task<TestDto> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var testEntity = new TestDto()
            {
                TestName = request.TestName,
                Time = request.Time,
                TotalPoint = request.TotalPoint,
                NumberOfQuestions = request.NumberOfQuestion,
                ImgLink = request.ImgLink,
                CreatedDate = DateTime.Now.ToString(),
                TestCategoryId = request.TestCategoryId,
                Ids = request.Ids
            };
            return await _testRepository.CreateAsync(testEntity);
        }
    }
}
