using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetPracticeTestResultQueryHandler : IRequestHandler<GetPracticeTestResultQuery, int>
    {
        private readonly IPracticeTestRepository _practiceTestRepository;

        public GetPracticeTestResultQueryHandler(IPracticeTestRepository practiceTestRepository)
        {
            _practiceTestRepository = practiceTestRepository;
        }
        public async Task<int> Handle(GetPracticeTestResultQuery request, CancellationToken cancellationToken)
        {
            List<AnswerSheetDto> answerSheetDtos = request.answerSheet.ToList();
            return await _practiceTestRepository.GetPracticeTestResult(answerSheetDtos);
        }
    }
}
