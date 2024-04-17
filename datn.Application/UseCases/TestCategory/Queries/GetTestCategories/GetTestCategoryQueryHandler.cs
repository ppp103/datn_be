using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetTestCategoryQueryHandler : IRequestHandler<GetTestCategoryQuery, List<TestCategory>>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;

        public GetTestCategoryQueryHandler(ITestCategoryRepository testCategoryRepository)
        {
            _testCategoryRepository = testCategoryRepository;
        }
        public async Task<List<TestCategory>> Handle(GetTestCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _testCategoryRepository.GetAllTestCategoryAsync();
        }
    }
}
