using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetQuestionByTestIdQueryHandler : IRequestHandler<GetQuestionByTestIdQuery, List<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetQuestionByTestIdQueryHandler(IQuestionRepository questionRepository)
        {

            _questionRepository = questionRepository;

        }
        public async Task<List<QuestionDto>> Handle(GetQuestionByTestIdQuery request, CancellationToken cancellationToken)
        {
            return await _questionRepository.GetQuestionByTestIdAsync(request.TestId);
        }
    }
}
