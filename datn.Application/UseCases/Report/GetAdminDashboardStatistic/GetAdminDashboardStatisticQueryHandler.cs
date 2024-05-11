using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetAdminDashboardStatisticQueryHandler : IRequestHandler<GetAdminDashboardStatisticQuery, AdminStaticsDto>
    {
        private readonly IReportRepository _reportRepository;
        public GetAdminDashboardStatisticQueryHandler(IReportRepository reportRepository)
        {

            _reportRepository = reportRepository;   

        }
        public async Task<AdminStaticsDto> Handle(GetAdminDashboardStatisticQuery request, CancellationToken cancellationToken)
        {
            return await _reportRepository.GetAdminStatistic();
        }
    }
}
