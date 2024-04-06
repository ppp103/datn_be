using AutoMapper;
using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetQuestionQueryHandler : IRequestHandler<GetQuestionQuery, List<QuestionVM>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionQueryHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<List<QuestionVM>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionRepository.GetAllQuestionsAsync();

            var questionList = _mapper.Map<List<QuestionVM>>(questions);

            return questionList;
        }
    }
}
