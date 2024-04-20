using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class AnswerSheetDto
    {
        public int QuestionId { get; set; }

        public string? ChosenOption {  get; set; }

        public int Point {  get; set; } 
    }
}
