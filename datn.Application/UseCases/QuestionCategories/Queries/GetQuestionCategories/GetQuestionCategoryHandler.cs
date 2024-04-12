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
    public class GetQuestionCategoryHandler : IRequestHandler<GetQuestionCategoryQuery, List<QuestionCategory>>
    {
        private readonly IQuestionCategoryRepository _questionCategoryRepository;
        private readonly IMapper _mapper;

        public GetQuestionCategoryHandler(IQuestionCategoryRepository questionCategoryRepository, IMapper mapper)
        {
            _questionCategoryRepository = questionCategoryRepository;
            _mapper = mapper;
        }
        public async Task<List<QuestionCategory>> Handle(GetQuestionCategoryQuery request, CancellationToken cancellationToken)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = AppConstants.MinPageSize;
            }
            if (request.PageSize == -1)
            {
                request.PageSize = AppConstants.MaxPageSize;
            }
            if (request.PageNumber == 0 || request.PageNumber == null)
            {
                request.PageNumber = 1;
            }
            var questionList = await _questionCategoryRepository.GetAllQuestionCategoryAsync();

            return questionList;
        }
    }
}
