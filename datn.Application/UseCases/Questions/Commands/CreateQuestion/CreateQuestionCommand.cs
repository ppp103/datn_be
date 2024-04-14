﻿using datn.Domain;
using MediatR;

namespace datn.Application
{
    public class CreateQuestionCommand : IRequest<Question>
    {
        public string Content { get; set; }

        public string? Option1 { get; set; }

        public string? Option2 { get; set; }

        public string? Option3 { get; set; }
                     
        public string? Option4 { get; set; }
                     
        public string? CorrectOption { get; set; }
                     
        public string? Explaination { get; set; }
                     
        public string? ImageUrl { get; set; }

        public int ChuDeId { get; set; }

        public int LoaiCauId { get; set; }

        //public int? Point { get; set; }

        public int DifficultyLevel { get; set; }

        //// Thời gian ước tính làm mỗi câu hỏi - tính theo phút
        //public int? Time { get; set; }
    }
}
