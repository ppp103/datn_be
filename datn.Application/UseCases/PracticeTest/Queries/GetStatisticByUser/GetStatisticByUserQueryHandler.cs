using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetStatisticByUserQueryHandler : IRequestHandler<GetStatisticByUserQuery, StatisticDto>
    {
        private readonly IPracticeTestRepository _practiceTestRepository;

        public GetStatisticByUserQueryHandler(IPracticeTestRepository practiceTestRepository)
        {
            _practiceTestRepository = practiceTestRepository;   
        }

        public async Task<StatisticDto> Handle(GetStatisticByUserQuery request, CancellationToken cancellationToken)
        {
            return await _practiceTestRepository.GetStatisticByUser(request.UserId, request.Time);
        }
    }
}
