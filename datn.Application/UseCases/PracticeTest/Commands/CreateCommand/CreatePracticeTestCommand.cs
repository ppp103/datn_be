using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreatePracticeTestCommand : IRequest<PracticeTestDto>
    {
        public int Id { get; set; }

        // Lần thi
        //public int TakeTimes { get; set; }

        // Thời gian làm
        public int Time { get; set; }

        public int UserId { get; set; }

        public int TestId { get; set; }

        public string? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public List<AnswerSheetDto> AnswerSheets { get; set; }
    }
}
