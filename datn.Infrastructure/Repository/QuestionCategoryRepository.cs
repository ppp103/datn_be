using datn.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class QuestionCategoryRepository : IQuestionCategoryRepository
    {
        private readonly QuestionDbContext _questionDbContext;

        public QuestionCategoryRepository(QuestionDbContext questionDbContext)
        {

            _questionDbContext = questionDbContext;

        }
        public async Task<List<QuestionCategory>> GetAllQuestionCategoryAsync()
        {
            return await _questionDbContext.QuestionCategories.OrderByDescending(q => q.Id).ToListAsync();

        }
    }
}
