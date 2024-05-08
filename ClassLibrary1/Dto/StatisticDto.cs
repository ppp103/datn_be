using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class StatisticDto
    {
        // Tổng số bài test đã làm
        public int TotalTakenTest {  get; set; }

        // Tổng thời gian luyện thi
        public int TotalPracticeTime {  get; set; }

        // Số lần làm bài - 1 test có thể làm nhiều lần => Tính tổng số lần làm
        public int TotalPracticeTestTaken { get; set; }

        // Tỉ lệ đúng theo từng bài test
        public List<ChartDto>? CorrectPercentage { get; set; }

        public double AverageCorrecPercent { get; set; }

        public double AverageTime {  get; set; }

        // Tỉ lệ đúng theo từng dạng câu hỏi
        public List<CorrectRateByTopicAndUserDto> CorrectPercentageByTopicAndUser { get; set; }
    }
}
