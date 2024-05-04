using datn.Application;
using datn.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace datn.Infrastructure
{
    public class PracticeTestRepository : IPracticeTestRepository
    {
        private readonly QuestionDbContext _questionDbContext;
        private readonly ITestRepository _testRepository;

        public PracticeTestRepository(QuestionDbContext questionDbContext, ITestRepository testRepository)
        {
            _questionDbContext = questionDbContext;
            _testRepository = testRepository;
        }
        public async Task<PracticeTestDto> CreateAsync(PracticeTestDto practiceTest)
        {
            using var transaction = _questionDbContext.Database.BeginTransaction();
            try
            {
                // Còn số lần thi sẽ kiểm tra bằng cách khác: Count BaiLuyen đã có userId và TestId chưa
                // Nếu có: +1 lần thi
                // Nếu không = 1
                var takeTimes = _questionDbContext.PracticeTest.Count(x => x.UserId == practiceTest.UserId && x.TestId == practiceTest.TestId) + 1;

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
                    CreatedBy = practiceTest.CreatedBy,
                    TakeTimes = takeTimes,
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
                var test = _questionDbContext.Tests.SingleOrDefault(test => test.Id == practiceTest.TestId);

                var result = new PracticeTestDto()
                {
                    Id = practiceTest.Id,
                    Time = practiceTest.Time,
                    Result = practiceTest.Result,
                    UserId = practiceTest.UserId,
                    TestId = practiceTest.TestId,
                    TestName = test?.TestName,
                    TotalPoint = test?.TotalPoint,
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

        public async Task<List<PracticeTestDto>> GetPracticeTestByTypeId(int id, int type)
        {
            // Type = 0 lấy theo userId, Type = 1 lấy theo testId
            // Get practiceTest
            var res = new List<PracticeTestDto>();
            var practiceTestList = new List<PracticeTest>();
            if (type == (int)PracticeTestEnum.TestId)
            {
                practiceTestList = _questionDbContext.PracticeTest.Where(x => x.TestId == id).ToList();
            }

            if (type == (int)PracticeTestEnum.UserId)
            {
                practiceTestList = _questionDbContext.PracticeTest.Where(x => x.UserId == id).ToList();
            }

            foreach (var practiceTest in practiceTestList)
            {
                var test = _questionDbContext.Tests.SingleOrDefault(test => test.Id == practiceTest.TestId);

                if (practiceTest != null)
                {
                    var result = new PracticeTestDto()
                    {
                        Id = practiceTest.Id,
                        Time = practiceTest.Time,
                        Result = practiceTest.Result,
                        UserId = practiceTest.UserId,
                        TestName = test?.TestName,
                        TotalPoint = test?.TotalPoint,
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
                return await Task.FromResult(res.OrderByDescending(x => x.Id).ToList());
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

        public async Task<StatisticDto> GetStatisticByUser(int userId, int time)
        {
            /// Thiếu lọc theo thời gian 
            /// và lấy tỉ lệ đúng các dạng câu hỏi
            /// 

            // Tổng số bài test
            var totalTakenTest = _questionDbContext.PracticeTest
                                    .Where(x => x.UserId == userId)
                                    .Select(x => x.TestId)
                                    .Distinct()
                                    .Count();

            // Tổng thời gian làm
            var totalTime = _questionDbContext.PracticeTest
                                .Where(x => x.UserId == userId)
                                .Select(x => x.Time)
                                .Sum();

            // <% đúng của từng bài luyện> <result / totalPoint>
            List<ChartDto> correctPercentage = new List<ChartDto>();
            var practiceTestList = _questionDbContext.PracticeTest.ToList();

            foreach (var practiceTest in practiceTestList)
            {
                //var test = _questionDbContext.Tests.Where(x => x.Id == practiceTest.TestId);
                var test = await _testRepository.GetByIdAsync(practiceTest.TestId);
                DateTime date;
                var formattedDate = "";
                if (DateTime.TryParseExact(practiceTest.CreatedDate, "MM/dd/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    formattedDate = date.ToString("dd/MM/yyyy");
                }

                if (practiceTest.UserId == userId)
                {
                    //var item = (double)practiceTest.Result / test.TotalPoint;

                    //correctPercentage.Add(new ChartDto
                    //{
                    //    Label = formattedDate,
                    //    Quantity = Math.Round(item * 100, 2)
                    //});

                    var numberOfCorrectAnswers = _questionDbContext.AnswerSheet.Where(x => x.IsCorrect && x.PracticeTestId == practiceTest.Id).Count();

                    var item = (double)numberOfCorrectAnswers / test.NumberOfQuestions;

                    correctPercentage.Add(new ChartDto
                    {
                        Label = formattedDate,
                        Quantity = Math.Round(item * 100, 2)
                    });

                }
            }

            // Tỉ lệ đúng theo chủ đề
            var correctRatesByTopicAndUser = new List<CorrectRateByTopicAndUserDto>();

            //List<Topic> topics = _questionDbContext.Topics.ToList();

            List<Topic> topics = (from topic in _questionDbContext.Topics
                                  join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
                                  join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
                                  select topic)
                                  .Distinct()
                                  .ToList();
                                 

            // Lấy ra chủ đề 
            foreach (var topicElement in topics)
            {
                var totalQuestionsByTopic =
                                (from topic in _questionDbContext.Topics
                                     join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
                                     join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
                                     join practiceTest in _questionDbContext.PracticeTest on answerSheet.PracticeTestId equals practiceTest.Id
                                     where practiceTest.UserId == userId && topic.Id == topicElement.Id
                                     select topic
                                 ).Count();

                var correctQuestionByTopic = 
                                (from topic in _questionDbContext.Topics
                                    join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
                                    join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
                                    join practiceTest in _questionDbContext.PracticeTest on answerSheet.PracticeTestId equals practiceTest.Id
                                    where practiceTest.UserId == userId && topic.Id == topicElement.Id && answerSheet.IsCorrect
                                    select topic
                                ).Count();

                if(totalQuestionsByTopic == 0)
                {
                    totalQuestionsByTopic = 1;
                }

                correctRatesByTopicAndUser.Add(new CorrectRateByTopicAndUserDto()
                {
                    TopicId = topicElement.Id,
                    TopicName = topicElement.Name,
                    CorrectRate = Math.Round((double)correctQuestionByTopic / totalQuestionsByTopic * 100, 2)
                });
            }
            // 
                       /// Return res
            var res = new StatisticDto()
            {
                TotalTakenTest = totalTakenTest,
                TotalPracticeTime = totalTime,
                CorrectPercentage = correctPercentage,
                CorrectPercentageByTopicAndUser = correctRatesByTopicAndUser,
            };

            return res;
            //throw new NotImplementedException();
        }
    }
}
