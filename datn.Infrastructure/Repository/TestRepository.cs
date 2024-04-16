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
    public class TestRepository : ITestRepository
    {
        private readonly QuestionDbContext _questionDbContext;

        public TestRepository(QuestionDbContext questionDbContext)
        {
            _questionDbContext = questionDbContext;
        }

        public async Task<TestDto> CreateAsync(TestDto test)
        {
            using var transaction = _questionDbContext.Database.BeginTransaction();
            try
            {
                // Tạo 1 test mới
                var newTest = new Test()
                {
                    TestName = test.TestName,
                    Time = test.Time,
                    TotalPoint = test.TotalPoint,
                    NumberOfQuestions = test.NumberOfQuestions,
                };
                var res = await _questionDbContext.Tests.AddAsync(newTest);
                await _questionDbContext.SaveChangesAsync();

                // Thêm câu hỏi vào bảng TestQuestion
                foreach (var id in test.Ids)
                {
                    var questionTestEntity = new QuestionTest()
                    {
                        QuestionId = id,
                        TestId = res.Entity.Id
                    };
                    await _questionDbContext.QuestionTests.AddAsync(questionTestEntity);
                }

                await _questionDbContext.SaveChangesAsync();
                transaction.Commit();
                return test;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagedList<TestDto>> GetAllTestPaggingAsync(int page, int pageSize)
        {
            var query = from test in _questionDbContext.Tests
                        select new TestDto
                        {
                            Id = test.Id,
                            TestName = test.TestName,
                            Time = test.Time,
                            TotalPoint = test.TotalPoint,
                            NumberOfQuestions = test.NumberOfQuestions,
                        };
            return await PagedList<TestDto>.CreateAsync(query.OrderByDescending(q => q.Id), page, pageSize);
        }


    }
}
