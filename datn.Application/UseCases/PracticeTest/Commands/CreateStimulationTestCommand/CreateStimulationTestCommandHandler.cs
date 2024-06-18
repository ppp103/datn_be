using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateStimulationTestCommandHandler : IRequestHandler<CreateStimulationTestCommand, PracticeTestDto>
    {
        private readonly IPracticeTestRepository _practiceTestRepository;

        public CreateStimulationTestCommandHandler(IPracticeTestRepository practiceTestRepository)
        {
            _practiceTestRepository = practiceTestRepository;
        }
        public async Task<PracticeTestDto> Handle(CreateStimulationTestCommand request, CancellationToken cancellationToken)
        {
            var practiceTest = new PracticeTestDto()
            {
                TestId = request.TestId,
                CreatedDate = DateTime.Now.ToString(),
                AnswerSheets = request.AnswerSheets
            };

            return await _practiceTestRepository.CreateStimulationTestAsync(practiceTest);
        }
    }
}
