using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetPracticeTestByTypeIdQueryHandler : IRequestHandler<GetPracticeTestByTypeIdQuery, List<PracticeTestDto>>
    {
        private readonly IPracticeTestRepository _practiceTestRepository;
        public GetPracticeTestByTypeIdQueryHandler(IPracticeTestRepository practiceTestRepository)
        {
            _practiceTestRepository = practiceTestRepository;
        }
        public async Task<List<PracticeTestDto>> Handle(GetPracticeTestByTypeIdQuery request, CancellationToken cancellationToken)
        {
            return await _practiceTestRepository.GetPracticeTestByTypeId(request.TestId, request.Type);
        }
    }
}
