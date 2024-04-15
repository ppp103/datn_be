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

        public async Task<Test> CreateAsync(Test test)
        {
            // Tạo 1 test mới
            await _questionDbContext.Tests.AddAsync(test);

            // Thêm câu hỏi vào bảng TestQuestion
            await _questionDbContext.SaveChangesAsync();
            return test;
        }

        public async Task<PagedList<TestDto>> GetAllTestPaggingAsync(int page, int pageSize)
        {
            var query = from test in _questionDbContext.Tests
                        select new TestDto
                        {
                            Id = test.Id,
                            TestName = test.TestName,
                            Time = test.Time,
                            NumberOfQuestions = test.NumberOfQuestions,
                        };
            return await PagedList<TestDto>.CreateAsync(query, page, pageSize);
        }


    }
}
