using AutoMapper;
using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace datn.Application
{
    public class QuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionVM>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionCommandHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            this._mapper = mapper;
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
                LoaiCauDeId = request.LoaiCauDeId,
            };
            var result = await _questionRepository.CreateAsync(questionEntity);
            return _mapper.Map<QuestionVM>(result);
        }
    }
}
