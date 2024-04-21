using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetPracticeTestByIdQueryHandler : IRequestHandler<GetPracticeTestByIdQuery, PracticeTestDto>
    {
        private readonly IPracticeTestRepository _practiceTestRepository;
        public GetPracticeTestByIdQueryHandler(IPracticeTestRepository practiceTestRepository)
        {
            _practiceTestRepository = practiceTestRepository;
        }
        public async Task<PracticeTestDto> Handle(GetPracticeTestByIdQuery request, CancellationToken cancellationToken)
        {
            return await _practiceTestRepository.GetPracticeTestById(request.Id);
        }
    }
}
