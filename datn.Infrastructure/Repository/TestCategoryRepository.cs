using datn.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class TestCategoryRepository : ITestCategoryRepository
    {
        private readonly QuestionDbContext _questionDbContext;

        public TestCategoryRepository(QuestionDbContext questionDbContext)
        {

            _questionDbContext = questionDbContext;

        }
        public async Task<List<TestCategory>> GetAllTestCategoryAsync()
        {
            return await _questionDbContext.TestCategory.OrderByDescending(q => q.Id).ToListAsync();
        }
    }
}
