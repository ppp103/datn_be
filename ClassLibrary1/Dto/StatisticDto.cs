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

        // Tỉ lệ đúng theo từng bài test
        public List<double>? CorrectPercentage { get; set; }  

        // Tỉ lệ đúng theo từng dạng câu hỏi
        public double CorrectPercentageByQuestionCategory { get; set; } 
    }
}
