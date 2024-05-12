using AutoMapper;
using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetQuestionQueryHandler : IRequestHandler<GetQuestionQuery, PagedList<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionQueryHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<QuestionDto>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = AppConstants.MinPageSize;
            }
            if (request.PageSize == -1)
            {
                request.PageSize = AppConstants.MaxPageSize;
            }
            if(request.PageNumber == 0 || request.PageNumber == null)
            {
                request.PageNumber = 1;
            }
            var questionList = await _questionRepository.GetAllQuestionPaggingAsync(
                request.PageNumber, 
                request.PageSize, 
                request.Keyword, 
                request.ChuDeId, 
                request.LoaiCauId,
                request.DifficultyLevel);
            //var questionList = await _questionRepository.GetAllQuestionPaggingAsync(request);
            //var res = _mapper.Map<Question>(questionList);
            return questionList;
        }
    }
}
