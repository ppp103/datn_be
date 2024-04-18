using datn.Application;
using datn.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class QuestionTestRepository : IQuestionTestRepository
    {
        private readonly QuestionDbContext _questionDbContext;
        public QuestionTestRepository(QuestionDbContext questionDbContext)
        {
            _questionDbContext = questionDbContext;
        }
        public async Task<List<QuestionTestDto>> GetQuestionTestsAsync(int testId)
        {
            //// lấy ra id những câu hỏi thuộc đề thi
            //var query = from q in _questionDbContext.Questions
            //            join questionTest in _questionDbContext.QuestionTests
            //            on q.Id equals questionTest.Id
            //            select new QuestionDto
            //            {
            //                Id = q.Id,
            //                Content = q.Content,
            //                Option1 = q.Option1,
            //                Option2 = q.Option2,
            //                Option3 = q.Option3,
            //                Option4 = q.Option4,
            //                CorrectOption = q.CorrectOption,
            //                Explaination = q.Explaination,
            //                LoaiCauId = q.LoaiCauId,
            //                Point = q.Point,
            //                DifficultyLevel = q.DifficultyLevel,
            //            };

            //// trả ra 1 list câu hỏi
            //return await query.ToListAsync();
            throw new NotImplementedException();
        }
    }
}
