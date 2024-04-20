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
                    TakeTimes = 1,
                };

                var res = await _questionDbContext.PracticeTest.AddAsync(newPracticeTest);
                await _questionDbContext.SaveChangesAsync();

                //// Thêm dữ liệu vào AnswerSheet <TraLoiCau>
                await _questionDbContext.AnswerSheet.AddRangeAsync(tuple.Item1);

                // Save change và commit
                await _questionDbContext.SaveChangesAsync();
                transaction.Commit();

                return practiceTest;
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

        public Task<PracticeTestDto> GetPracticeTestById(int id)
        {
            throw new NotImplementedException();
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
            foreach(var answerSheetDto in answerSheetDtos)
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
