using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreatePracticeTestCommandHandler : IRequestHandler<CreatePracticeTestCommand, PracticeTestDto>
    {
        private readonly IPracticeTestRepository _practiceTestRepository;

        public CreatePracticeTestCommandHandler(IPracticeTestRepository practiceTestRepository)
        {

            _practiceTestRepository = practiceTestRepository;

        }
        public async Task<PracticeTestDto> Handle(CreatePracticeTestCommand request, CancellationToken cancellationToken)
        {
            var practiceTest = new PracticeTestDto()
            {
                Time = request.Time,
                UserId = request.UserId,
                TestId = request.TestId,
                CreatedDate = DateTime.Now.ToString(),
                CreatedBy = request.CreatedBy,
                AnswerSheets = request.AnswerSheets
        };

            return await _practiceTestRepository.CreateAsync(practiceTest);
        }
    }
}
