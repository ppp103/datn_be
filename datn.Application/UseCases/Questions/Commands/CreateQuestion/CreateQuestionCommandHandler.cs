using AutoMapper;
using datn.Domain;
using MediatR;

namespace datn.Application
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionVM>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<QuestionVM> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
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
            };
            var result = await _questionRepository.CreateAsync(questionEntity);
            return _mapper.Map<QuestionVM>(result);
        }
    }
}
