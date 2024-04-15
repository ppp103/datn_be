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
            return await _questionDbContext.Questions.Where(model => model.Id == id).ExecuteDeleteAsync();
        }

        //public async Task<PagedList<Question>> GetAllQuestionPaggingAsync(int page, int pageSize, string keyWord)
        //{
        //    //IQueryable<Question> query = _questionDbContext.Questions;
        //    //IQueryable<Topic> topic = _questionDbContext.Topics;
        //    //IQueryable<QuestionCategory> category = _questionDbContext.QuestionCategories;
        //    var query = from question in _questionDbContext.Questions
        //                join topic in _questionDbContext.Topics
        //                on question.ChuDeId equals topic.Id
        //                join category in _questionDbContext.QuestionCategories
        //                on question.LoaiCauId equals category.Id
        //                select new Question
        //                {
        //                    Id = question.Id,
        //                    Content = question.Content,
        //                    ChuDe = topic.Name,
        //                    LoaiCau = category.QuestionCategoryName,
        //                    Option1 = question.Option1,
        //                    Option2 = question.Option2,
        //                    Option3 = question.Option3,
        //                    Option4 = question.Option4,
        //                    CorrectOption = question.CorrectOption,
        //                    Explaination = question.Explaination,
        //                    ChuDeId = question.ChuDeId,
        //                    LoaiCauId = question.LoaiCauId
        //                };

        //    if (!string.IsNullOrWhiteSpace(keyWord)){
        //        query = query.Where(q => EF.Functions.Like(q.Content, $"%{keyWord}%"));
        //    }

        //    var res = await PagedList<Question>.CreateAsync(query.OrderByDescending(q => q.Id), page, pageSize);
        //    return res;
        //}

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
