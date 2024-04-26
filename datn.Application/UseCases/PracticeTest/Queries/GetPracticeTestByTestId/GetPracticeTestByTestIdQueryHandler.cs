using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetPracticeTestByTestIdQueryHandler : IRequestHandler<GetPracticeTestByTestIdQuery, List<PracticeTestDto>>
    {
        private readonly IPracticeTestRepository _practiceTestRepository;
        public GetPracticeTestByTestIdQueryHandler(IPracticeTestRepository practiceTestRepository)
        {
            _practiceTestRepository = practiceTestRepository;
        }
        public async Task<List<PracticeTestDto>> Handle(GetPracticeTestByTestIdQuery request, CancellationToken cancellationToken)
        {
            return await _practiceTestRepository.GetPracticeTestByTestId(request.TestId);
        }
    }
}
