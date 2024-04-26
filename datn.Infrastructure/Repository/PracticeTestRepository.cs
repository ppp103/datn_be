using datn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class PracticeTestRepository : IPracticeTestRepository
    {
        private readonly QuestionDbContext _questionDbContext;

        public PracticeTestRepository(QuestionDbContext questionDbContext)
        {
            _questionDbContext = questionDbContext;
        }
        public async Task<PracticeTestDto> CreateAsync(PracticeTestDto practiceTest)
        {
            using var transaction = _questionDbContext.Database.BeginTransaction();
            try
            {
                // Còn số lần thi sẽ kiểm tra bằng cách khác: kiểm tra trong BaiLuyen đã có userId và TestId chưa
                // Nếu có: +1 lần thi
                // Nếu không = 1

                // Lấy ra kết quả
                var tuple = GetPracticeTestResultFunction(practiceTest.AnswerSheets);

                // Thêm practiceTest mới
                var newPracticeTest = new PracticeTest
                {
                    Time = practiceTest.Time,
                    Result = tuple.Item2,
                    UserId = practiceTest.UserId,
                    TestId = practiceTest.TestId,
                    CreatedDate = DateTime.Now.ToString(),
                    TakeTimes = 1, // tạm thời fix cứng
                };

                var practiceTestEntity = await _questionDbContext.PracticeTest.AddAsync(newPracticeTest);
                await _questionDbContext.SaveChangesAsync();

                //// Thêm dữ liệu vào AnswerSheet <TraLoiCau>
                foreach (var item in tuple.Item1)
                {
                    item.PracticeTestId = practiceTestEntity.Entity.Id;
                }
                await _questionDbContext.AnswerSheet.AddRangeAsync(tuple.Item1);

                // Save change và commit
                await _questionDbContext.SaveChangesAsync();
                transaction.Commit();

                //return practiceTest;
                var result = new PracticeTestDto()
                {
                    Id = practiceTestEntity.Entity.Id,
                    Result = tuple.Item2
                };

                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);

            }
        }

        public Task<List<PracticeTestDto>> GetAllPracticeTest()
        {
            throw new NotImplementedException();
        }

        public async Task<PracticeTestDto> GetPracticeTestById(int id)
        {
            // Get practiceTest
            var practiceTest = _questionDbContext.PracticeTest.FirstOrDefault(p => p.Id == id);
            // Get answerSheet
            var query = from answerSheet in _questionDbContext.AnswerSheet
                        join question in _questionDbContext.Questions
                        on answerSheet.QuesitonId equals question.Id
                        where answerSheet.PracticeTestId == id
                        select new AnswerSheetDto()
                        {
                            Id = answerSheet.Id,
                            QuestionId = answerSheet.QuesitonId,
                            ChosenOption = answerSheet.ChosenOption,
                            IsCorrect = answerSheet.IsCorrect,
                            Explaination = question.Explaination,
                            DifficultyLevel = question.DifficultyLevel,
                            Point = question.Point,
                            Option1 = question.Option1,
                            Option2 = question.Option2,
                            Option3 = question.Option3,
                            Option4 = question.Option4,
                            Content = question.Content,
                            LoaiCauId = question.LoaiCauId,
                            CorrectOption = question.CorrectOption,
                        };
            if (practiceTest != null)
            {
                var result = new PracticeTestDto()
                {
                    Id = practiceTest.Id,
                    Time = practiceTest.Time,
                    Result = practiceTest.Result,
                    UserId = practiceTest.UserId,
                    TestId = practiceTest.TestId,
                    CreatedDate = practiceTest.CreatedDate,
                    CreatedBy = practiceTest.CreatedBy,
                    AnswerSheets = query.ToList(),
                };

                return await Task.FromResult(result);
            }
            else
            {
                throw new Exception("Bài luyện không tồn tại");
            }
        }

        public async Task<List<PracticeTestDto>> GetPracticeTestByTestId(int testId)
        {
            // Get practiceTest
            var res = new List<PracticeTestDto>();
            var practiceTestList = _questionDbContext.PracticeTest.Where(x => x.TestId == testId).ToList();

            foreach (var practiceTest in practiceTestList)
            {

                //var practiceTest = _questionDbContext.PracticeTest.FirstOrDefault(p => p.Id == id);
                // Get answerSheet
                //var query = from answerSheet in _questionDbContext.AnswerSheet
                //            join question in _questionDbContext.Questions
                //            on answerSheet.QuesitonId equals question.Id
                //            where answerSheet.PracticeTestId == practiceTest.Id
                //            select new AnswerSheetDto()
                //            {
                //                Id = answerSheet.Id,
                //                QuestionId = answerSheet.QuesitonId,
                //                ChosenOption = answerSheet.ChosenOption,
                //                IsCorrect = answerSheet.IsCorrect,
                //                Explaination = question.Explaination,
                //                DifficultyLevel = question.DifficultyLevel,
                //                Point = question.Point,
                //                Option1 = question.Option1,
                //                Option2 = question.Option2,
                //                Option3 = question.Option3,
                //                Option4 = question.Option4,
                //                Content = question.Content,
                //                LoaiCauId = question.LoaiCauId,
                //                CorrectOption = question.CorrectOption,
                //            };
                if (practiceTest != null)
                {
                    var result = new PracticeTestDto()
                    {
                        Id = practiceTest.Id,
                        Time = practiceTest.Time,
                        Result = practiceTest.Result,
                        UserId = practiceTest.UserId,
                        TestId = practiceTest.TestId,
                        CreatedDate = practiceTest.CreatedDate,
                        CreatedBy = practiceTest.CreatedBy,
                        //AnswerSheets = query.ToList(),
                    };
                    res.Add(result);
                }
            }
            if (res != null)
            {
                return await Task.FromResult(res);
            }
            else
            {
                throw new Exception("Bài luyện không tồn tại");
            }

        }

        public Tuple<List<AnswerSheet>, int> GetPracticeTestResultFunction(List<AnswerSheetDto> answerSheetDtos)
        {
            List<AnswerSheet> answerSheets = new List<AnswerSheet>();
            int result = 0;
            foreach (var answerSheetDto in answerSheetDtos)
            {
                // Lấy ra câu hỏi
                var question = _questionDbContext.Questions.FirstOrDefault(q => q.Id == answerSheetDto.QuestionId);

                if (question != null)
                {
                    // Kiểm tra đáp án
                    var isCorrect = question.CorrectOption == answerSheetDto.ChosenOption;
                    var answerSheet = new AnswerSheet()
                    {
                        QuesitonId = answerSheetDto.QuestionId,
                        ChosenOption = answerSheetDto.ChosenOption,
                        IsCorrect = question?.CorrectOption == answerSheetDto.ChosenOption,
                    };

                    // Tính điểm
                    if (isCorrect)
                        result += answerSheetDto.Point;

                    answerSheets.Add(answerSheet);
                }
            }

            // Return điểm
            return new Tuple<List<AnswerSheet>, int>(answerSheets, result);
        }


        public Task<int> GetPracticeTestResult(List<AnswerSheetDto> answerSheetDtos)
        {
            List<AnswerSheet> answerSheets = new List<AnswerSheet>();
            int result = 0;
            foreach (var answerSheetDto in answerSheetDtos)
            {
                // Lấy ra câu hỏi
                var question = _questionDbContext.Questions.FirstOrDefault(q => q.Id == answerSheetDto.QuestionId);
                // Kiểm tra đáp án
                var answerSheet = new AnswerSheet()
                {
                    QuesitonId = answerSheetDto.QuestionId,
                    ChosenOption = answerSheetDto.ChosenOption,
                    IsCorrect = question?.CorrectOption == answerSheetDto.ChosenOption
                };

                // Tính điểm
                if (answerSheet.IsCorrect) result += answerSheetDto.Point;
                answerSheets.Add(answerSheet);
            }

            // Thêm answerSheet vào CSDL
            _questionDbContext.AnswerSheet.AddRangeAsync(answerSheets);
            _questionDbContext.SaveChangesAsync();

            // Return điểm
            return Task.FromResult(result);
        }
    }
}
