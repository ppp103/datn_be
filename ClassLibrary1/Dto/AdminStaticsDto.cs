using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class AdminStaticsDto
    {
        public int TotalUsers {  get; set; }
        public int TotalTests { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalPracticeTests { get; set; }
        public List<ChartDto> PracticeTestsChart { get; set; }
    }
}
