using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public interface IReportRepository
    {
        Task<StatisticDto> GetStatisticByUser(int userId, int time);
    }
}
