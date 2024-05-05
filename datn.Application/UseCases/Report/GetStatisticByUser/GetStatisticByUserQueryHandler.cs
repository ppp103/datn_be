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
        private readonly IReportRepository _reportRepository;

        public GetStatisticByUserQueryHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;   
        }

        public async Task<StatisticDto> Handle(GetStatisticByUserQuery request, CancellationToken cancellationToken)
        {
            return await _reportRepository.GetStatisticByUser(request.UserId, request.Time);
        }
    }
}
