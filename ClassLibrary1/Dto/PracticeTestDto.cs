using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class PracticeTestDto
    {
        public int Id { get; set; }

        // Lần thi
        //public int TakeTimes { get; set; }

        // Thời gian làm
        public int Time { get; set; }

        public int Result { get; set; }

        public int UserId { get; set; }

        public int TestId { get; set; }

        public int TakeTimes { get; set; }

        public string? UserName { get; set; }

        public string? TestName { get; set; } 

        public int? TotalPoint {  get; set; }   

        public string? ImgLink { get; set; }

        public string? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public List<AnswerSheetDto> AnswerSheets { get; set; }
    }
}
