using MediatR;

namespace datn.Application
{
    public class CreateQuestionCommand : IRequest<QuestionVM>
    {
        public string Content { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public string CorrectOption { get; set; }

        public string Explaination { get; set; }

        public string ImageUrl { get; set; }

        public int ChuDeId { get; set; }

        public int LoaiCauDeId { get; set; }

    }
}
