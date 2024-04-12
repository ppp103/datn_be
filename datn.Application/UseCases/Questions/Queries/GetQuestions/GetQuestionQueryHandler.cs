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
            //if (request.PageSize == 0)
            //{
            //    request.PageSize = AppConstants.MinPageSize;
            //}
            //if (request.PageSize == -1)
            //{
            //    request.PageSize = AppConstants.MaxPageSize;
            //}

            //Expression<Func<Question, bool>> filter = c =>
            // (string.IsNullOrEmpty(request.Keyword) || c.Content.ToLower().Contains(request.Keyword.ToLower()))
            // && (request.ChuDeId == null || c.ChuDeId == request.ChuDeId)
            // && (request.LoaiCauId == null || c.LoaiCauId <= request.LoaiCauId);

            //var questions = await _questionRepository.GetAllQuestionsAsync();

            ////var questionList = _mapper.Map<IQueryable<QuestionVM>>(questions);
            //var questionList = questions
            //    .Select(q => new QuestionVM
            //    {
            //        Id = q.Id,
            //        Content = q.Content,
            //        Option1 = q.Option1,
            //        Option2 = q.Option2,
            //        Option3 = q.Option3,
            //        Option4 = q.Option4,
            //        CorrectOption = q.CorrectOption,
            //        Explaination = q.Explaination,
            //        ChuDeId = q.ChuDeId,
            //        LoaiCauId = q.LoaiCauId
            //    }).AsQueryable();
            //return await PagedList<QuestionVM>.CreateAsync(questionList, request.PageNumber, request.PageSize);
            ////return await questionList.OrderByDescending(x => x.Id).CreateAsync();
            ///
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
                request.LoaiCauId);
            //var questionList = await _questionRepository.GetAllQuestionPaggingAsync(request);
            //var res = _mapper.Map<Question>(questionList);
            return questionList;
        }
    }
}
