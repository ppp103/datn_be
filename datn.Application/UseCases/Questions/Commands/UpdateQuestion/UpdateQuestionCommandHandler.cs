using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, int>
    {
        private readonly IQuestionRepository _questionRepository;

        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public async Task<int> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var UpdateQuestionEntity = new Question()
            {
                Id = request.Id,
                Content = request.Content,
                Option1 = request.Option1,
                Option2 = request.Option2,
                Option3 = request.Option3,
                Option4 = request.Option4,
                CorrectOption = request.CorrectOption,
                Explaination = request.Explaination,
                ImageUrl = request.ImageUrl,
                ChuDeId = request.ChuDeId,
                LoaiCauId = request.LoaiCauId,
                Point = request.Point,
                DifficultyLevel = request.DifficultyLevel,
                Time = request.Time,
            };

            return await _questionRepository.UpdateAsync(request.Id, UpdateQuestionEntity);
        }
    }
}
