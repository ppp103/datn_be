using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class AnswerSheetDto
    {
        public int Id { get; set; } 

        public int QuestionId { get; set; }

        public string? ChosenOption {  get; set; }

        public bool IsCorrect { get; set; }

        public int Point {  get; set; } 

        public string? Explaination { get;set; }

        public int? DifficultyLevel { get; set; }

        public string? Content { get; set; }

        public string? Option1 { get; set; }

        public string? Option2 { get; set; }

        public string? Option3 { get; set; }

        public string? Option4 { get; set; }

        public string? CorrectOption { get; set; }  

        public int? LoaiCauId { get; set; }
    }
}
