﻿using datn.Application;
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
                    CreatedBy = test.CreatedBy,
                    CreatedDate = test.CreatedDate,
                    ImgLink = test.ImgLink,
                    NumberOfQuestions = test.NumberOfQuestions,
                    TestCategoryId = test.TestCategoryId
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

        public async Task<PagedList<TestDto>> GetAllTestPaggingAsync(int page, int pageSize, string keyWord, int? testCategoryId)
        {
            var query = from test in _questionDbContext.Tests
                        join
                        testCategory in _questionDbContext.TestCategory
                        on test.TestCategoryId equals testCategory.Id
                        select new TestDto
                        {
                            Id = test.Id,
                            TestName = test.TestName,
                            Time = test.Time,
                            TotalPoint = test.TotalPoint,
                            CreatedBy = test.CreatedBy,
                            CreatedDate = test.CreatedDate,
                            ImgLink = AppConstants.TEST_IMAGE_ROOT + (test.ImgLink ?? "avatar-default.png"),
                            TestCategoryName = testCategory.TestCategoryName,
                            TestCategoryId = testCategory.Id,
                            NumberOfQuestions = test.NumberOfQuestions,
                        };

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(q => EF.Functions.Like(q.TestName, $"%{keyWord}%"));
            }


            if (testCategoryId != null && testCategoryId != 0)
            {
                query = query.Where(q => q.TestCategoryId == testCategoryId);
            }

            return await PagedList<TestDto>.CreateAsync(query.OrderByDescending(q => q.Id), page, pageSize);
        }

        public async Task<TestDto> GetByIdAsync(int id)
        {
            var query = from test in _questionDbContext.Tests join 
                        testCategory in _questionDbContext.TestCategory
                        on test.TestCategoryId equals testCategory.Id
                        select new TestDto { 
                            Id = test.Id, 
                            TestName = test.TestName,
                            Time = test.Time,
                            TotalPoint = test.TotalPoint,
                            CreatedBy = test.CreatedBy,
                            CreatedDate = test.CreatedDate,
                            ImgLink = AppConstants.TEST_IMAGE_ROOT + (test.ImgLink ?? "avatar-default.png"), 
                            NumberOfQuestions = test.NumberOfQuestions,
                            TestCategoryId = testCategory.Id,
                            TestCategoryName = testCategory.TestCategoryName
                        };
            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
