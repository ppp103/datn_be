using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class CorrectRateByTopicAndUserDto
    {
        public int TopicId { get; set; } 
        public string TopicName { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswers { get; set; }
        public double CorrectRate { get; set; } 
    }
}
