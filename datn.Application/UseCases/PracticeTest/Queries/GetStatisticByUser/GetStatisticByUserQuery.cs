using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetStatisticByUserQuery : IRequest<StatisticDto>
    {
        public int UserId { get; set; } 

        // Thống kê kết quả trong Time ngày
        public int Time { get; set; }
    }
}
