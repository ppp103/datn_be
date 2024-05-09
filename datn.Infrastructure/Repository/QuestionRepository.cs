using datn.Application;
using datn.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class QuestionRepository : IQuestionRepository
    {   
        private readonly QuestionDbContext _questionDbContext;

        public QuestionRepository(QuestionDbContext questionDbContext)
        {

            _questionDbContext = questionDbContext;

        }

        public async Task<Question> CreateAsync(Question question)
        {
            await _questionDbContext.Questions.AddAsync(question);
            await _questionDbContext.SaveChangesAsync();
            return question;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var question = await _questionDbContext.QuestionTests.SingleOrDefaultAsync(x => x.QuestionId == id);
            if (question != null)
            {
                throw new Domain.SystemException("Xoá thất bại! Câu hỏi đang được sử dụng");
            }
            return await _questionDbContext.Questions.Where(model => model.Id == id).ExecuteDeleteAsync();
        }

        public async Task<PagedList<QuestionDto>> GetAllQuestionPaggingAsync(int page, int pageSize, string keyWord, int? chuDeId, int? loaiCauId, int? difficultyLevel)
        {
            var query = from question in _questionDbContext.Questions
                        join topic in _questionDbContext.Topics
                        on question.ChuDeId equals topic.Id
                        join category in _questionDbContext.QuestionCategories
                        on question.LoaiCauId equals category.Id
                        select new QuestionDto
                        {
                            Id = question.Id,
                            Content = question.Content,
                            ChuDe = topic.Name,
                            LoaiCau = category.QuestionCategoryName,
                            Option1 = question.Option1,
                            Option2 = question.Option2,
                            Option3 = question.Option3,
                            Option4 = question.Option4,
                            CorrectOption = question.CorrectOption,
                            Explaination = question.Explaination,
                            ChuDeId = question.ChuDeId,
                            LoaiCauId = question.LoaiCauId,
                            Point = question.Point,
                            Time = question.Time,
                            DifficultyLevel = question.DifficultyLevel,
                        };

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(q => EF.Functions.Like(q.Content, $"%{keyWord}%"));
            }

            if(chuDeId != null && chuDeId != 0)
            {
                query = query.Where(q => q.ChuDeId == chuDeId);
            }

            if (loaiCauId != null && loaiCauId != 0)
            {
                query = query.Where(q => q.LoaiCauId == loaiCauId);
            }

            if (difficultyLevel != null && difficultyLevel != 0)
            {
                query = query.Where(q => q.DifficultyLevel == difficultyLevel);
            }

            var res = await PagedList<QuestionDto>.CreateAsync(query.OrderByDescending(q => q.Id), page, pageSize);
            return res;
        }


        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await _questionDbContext.Questions.OrderByDescending(q=>q.Id).ToListAsync();
        }

        public async Task<QuestionDto> GetByIdAsync(int id)
        {
            var query = from question in _questionDbContext.Questions
                        join topic in _questionDbContext.Topics
                        on question.ChuDeId equals topic.Id
                        join category in _questionDbContext.QuestionCategories
                        on question.LoaiCauId equals category.Id
                        where question.Id == id
                        select new QuestionDto
                        {
                            Id = question.Id,
                            Content = question.Content,
                            ChuDe = topic.Name,
                            LoaiCau = category.QuestionCategoryName,
                            Option1 = question.Option1,
                            Option2 = question.Option2,
                            Option3 = question.Option3,
                            Option4 = question.Option4,
                            CorrectOption = question.CorrectOption,
                            Explaination = question.Explaination,
                            ChuDeId = question.ChuDeId,
                            LoaiCauId = question.LoaiCauId,
                            Point = question.Point,
                            Time = question.Time,
                            DifficultyLevel = question.DifficultyLevel,

                        };

            return await query.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<QuestionDto>> GetQuestionByTestIdAsync(int testId)
        {
            // lấy ra id những câu hỏi thuộc đề thi
            var query = from q in _questionDbContext.Questions
                        join questionTest in _questionDbContext.QuestionTests
                        on q.Id equals questionTest.QuestionId
                        join test in _questionDbContext.Tests
                        on questionTest.TestId equals test.Id
                        where test.Id == testId
                        select new QuestionDto
                        {
                            Id = q.Id,
                            Content = q.Content,
                            Option1 = q.Option1,
                            Option2 = q.Option2,
                            Option3 = q.Option3,
                            Option4 = q.Option4,
                            CorrectOption = q.CorrectOption,
                            Explaination = q.Explaination,
                            LoaiCauId = q.LoaiCauId,
                            Point = q.Point,
                            DifficultyLevel = q.DifficultyLevel,
                        };

            // trả ra 1 list câu hỏi
            return await query.ToListAsync();
        }

        public async Task<int> UpdateAsync(int id, Question question)
        {
            return await _questionDbContext.Questions.Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters.
                    SetProperty(m => m.Id, question.Id).
                    SetProperty(m => m.Content, question.Content).
                    SetProperty(m => m.Option1, question.Option1).
                    SetProperty(m => m.Option2, question.Option2).
                    SetProperty(m => m.Option3, question.Option3).
                    SetProperty(m => m.Option4, question.Option4).
                    SetProperty(m => m.CorrectOption, question.CorrectOption).
                    SetProperty(m => m.Explaination, question.Explaination).
                    SetProperty(m => m.ImageUrl, question.ImageUrl).
                    SetProperty(m => m.ChuDeId, question.ChuDeId).
                    SetProperty(m => m.LoaiCauId, question.LoaiCauId)
                );
        }
    }
}
