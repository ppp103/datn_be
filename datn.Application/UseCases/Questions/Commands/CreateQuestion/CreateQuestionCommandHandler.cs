using AutoMapper;
using datn.Domain;
using MediatR;

namespace datn.Application
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Question>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<Question> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var point = 0;
            var time = 0;
            switch (request.DifficultyLevel)
            {
                case 1: 
                    point = 5;
                    time = 2;
                    break;
                case 2:
                    point = 10;
                    time = 3;
                    break;
                case 3:
                    point = 15;
                    time = 4;
                    break;
            }
            var questionEntity = new Question()
            {
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
                DifficultyLevel = request.DifficultyLevel,
                Point = point,
                Time = time,
            };

            var result = await _questionRepository.CreateAsync(questionEntity);
            return result;
        }
    }
}
