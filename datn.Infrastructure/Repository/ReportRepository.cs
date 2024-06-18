using datn.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace datn.Infrastructure
{
    public class ReportRepository : IReportRepository
    {
        private readonly QuestionDbContext _questionDbContext;
        private readonly ITestRepository _testRepository;

        public ReportRepository(QuestionDbContext questionDbContext, ITestRepository testRepository)
        {
            _questionDbContext = questionDbContext;
            _testRepository = testRepository;
        }

        public async Task<AdminStaticsDto> GetAdminStatistic()
        {
            var totalUsers = _questionDbContext.User.Count();
            var totalTests = _questionDbContext.Tests.Count();
            var totalQuestions = _questionDbContext.Questions.Count();
            //var totalPracticeTests = _questionDbContext.PracticeTest.Count();

            var query = from practiceTest in _questionDbContext.PracticeTest
                        join user in _questionDbContext.User on practiceTest.UserId equals user.Id
                        join test in _questionDbContext.Tests on practiceTest.TestId equals test.Id
                        select new PracticeTestDto()
                        {
                            Id = practiceTest.Id,
                            CreatedDate = practiceTest.CreatedDate,
                        };

            var totalPracticeTests = query.Count();

            // Tạo một Dictionary để lưu trữ số lượng bài test cho mỗi ngày
            var tests = query.ToList();

            Dictionary<string, int> testCountByDate = new Dictionary<string, int>();

            // Đếm số lượng bài test cho mỗi ngày
            foreach (var test in tests)
            {
                if (test.CreatedDate == null) test.CreatedDate = DateTime.Now.ToString();
                string date = DateTime.ParseExact(test.CreatedDate, "MM/dd/yyyy HH:mm:ss", null).ToString("MM/dd/yyyy");


                if (testCountByDate.ContainsKey(date))
                {
                    testCountByDate[date]++;
                }
                else
                {
                    testCountByDate[date] = 1;
                }
            }

            var totalPracticeTestByDate = testCountByDate.Select(pair => new ChartDto() { Label = pair.Key, Quantity = pair.Value }).Take(7).ToList();

            return new AdminStaticsDto()
            {
                TotalUsers = totalUsers,
                TotalTests = totalTests,
                TotalQuestions = totalQuestions,
                TotalPracticeTests = totalPracticeTests,
                PracticeTestsChart = totalPracticeTestByDate
            };
        }

        //public async Task<StatisticDto> GetStatisticByUser(int userId, int time)
        //{
        //    /// Thiếu lọc theo thời gian 

        //    // Tổng số bài test đã làm
        //    var totalTakenTest = _questionDbContext.PracticeTest
        //                            .Where(x => x.UserId == userId)
        //                            .Select(x => x.TestId)
        //                            .Distinct()
        //                            .Count();

        //    // Số lần làm bài - tính cả làm lại 1 bài test
        //    var totalPracticeTimes = _questionDbContext.PracticeTest
        //                            .Where(x => x.UserId == userId)
        //                            .Distinct()
        //                            .Count();
        //    // Lần làm bài gần nhất
        //    var lastestPracticeTime = _questionDbContext.PracticeTest.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == userId);

        //    // Tổng thời gian làm
        //    var totalTime = _questionDbContext.PracticeTest
        //                        .Where(x => x.UserId == userId)
        //                        .Select(x => x.Time)
        //                        .Sum();

        //    // <% đúng của từng bài luyện> <result / totalPoint>
        //    List<ChartDto> correctPercentage = new List<ChartDto>();
        //    var practiceTestList = _questionDbContext.PracticeTest.ToList();

        //    foreach (var practiceTest in practiceTestList)
        //    {
        //        var test = await _testRepository.GetByIdAsync(practiceTest.TestId);
        //        DateTime date;
        //        var formattedDate = "";
        //        if (DateTime.TryParseExact(practiceTest.CreatedDate, "MM/dd/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out date))
        //        {
        //            formattedDate = date.ToString("dd/MM/yyyy");
        //        }

        //        if (practiceTest.UserId == userId)
        //        {
        //            //var item = (double)practiceTest.Result / test.TotalPoint;

        //            //correctPercentage.Add(new ChartDto
        //            //{
        //            //    Label = formattedDate,
        //            //    Quantity = Math.Round(item * 100, 2)
        //            //});

        //            var numberOfCorrectAnswers = _questionDbContext.AnswerSheet.Where(x => x.IsCorrect && x.PracticeTestId == practiceTest.Id).Count();

        //            var item = (double)numberOfCorrectAnswers / test.NumberOfQuestions;

        //            correctPercentage.Add(new ChartDto
        //            {
        //                Label = formattedDate,
        //                Time = practiceTest.Time,
        //                Quantity = Math.Round(item * 100, 2)
        //            });
        //        }
        //    }

        //    // Tỉ lệ đúng theo chủ đề
        //    var correctRatesByTopicAndUser = new List<CorrectRateByTopicAndUserDto>();

        //    //List<Topic> topics = _questionDbContext.Topics.ToList();

        //    List<Topic> topics = (from topic in _questionDbContext.Topics
        //                          join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
        //                          join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
        //                          select topic)
        //                          .Distinct()
        //                          .ToList();


        //    // Lấy ra chủ đề 
        //    foreach (var topicElement in topics)
        //    {
        //        var totalQuestionsByTopic =
        //                        (from topic in _questionDbContext.Topics
        //                         join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
        //                         join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
        //                         join practiceTest in _questionDbContext.PracticeTest on answerSheet.PracticeTestId equals practiceTest.Id
        //                         where practiceTest.UserId == userId && topic.Id == topicElement.Id
        //                         select topic
        //                         ).Count();

        //        var correctQuestionByTopic =
        //                        (from topic in _questionDbContext.Topics
        //                         join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
        //                         join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
        //                         join practiceTest in _questionDbContext.PracticeTest on answerSheet.PracticeTestId equals practiceTest.Id
        //                         where practiceTest.UserId == userId && topic.Id == topicElement.Id && answerSheet.IsCorrect
        //                         select topic
        //                        ).Count();

        //        if (totalQuestionsByTopic == 0)
        //        {
        //            totalQuestionsByTopic = 1;
        //        }

        //        correctRatesByTopicAndUser.Add(new CorrectRateByTopicAndUserDto()
        //        {
        //            TopicId = topicElement.Id,
        //            TopicName = topicElement.Name,
        //            CorrectAnswers = correctQuestionByTopic,
        //            TotalAnswers = totalQuestionsByTopic,
        //            CorrectRate = Math.Round((double)correctQuestionByTopic / totalQuestionsByTopic * 100, 2)
        //        });
        //    }

        //    // Thời gian trung bình
        //    var averageTime = Math.Round((double)correctPercentage.Sum(x => x.Time) / correctPercentage.Count(), 2);

        //    // Độ chính xác trung bình
        //    var averageCorrecPercent = Math.Round((double)correctPercentage.Sum(x => x.Quantity) / correctPercentage.Count(), 2);

        //    var totalPracticeTests = correctPercentage.Count();
        //    // 
        //    /// Return res
        //    var res = new StatisticDto()
        //    {
        //        TotalTakenTest = totalTakenTest,
        //        TotalPracticeTime = totalTime,
        //        AverageCorrecPercent = averageCorrecPercent,
        //        AverageTime = averageTime,
        //        TotalPracticeTestTaken = totalPracticeTests,
        //        CorrectPercentage = correctPercentage,
        //        CorrectPercentageByTopicAndUser = correctRatesByTopicAndUser,
        //    };

        //    return res;
        //}

        public async Task<StatisticDto> GetStatisticByUser(int userId, int time)
        {
            // Tính toán thời gian bắt đầu dựa trên tham số `time`
            DateTime startTime = DateTime.Now.AddDays(-time); // Nếu time là số ngày
                                                              // Nếu time là số tuần: DateTime startTime = DateTime.Now.AddDays(-time * 7);
                                                              // Nếu time là số tháng: DateTime startTime = DateTime.Now.AddMonths(-time);

            // Lấy toàn bộ dữ liệu vào bộ nhớ
            var practiceTestList = await _questionDbContext.PracticeTest
                                        .Where(x => x.UserId == userId)
                                        .ToListAsync();

            // Lọc dữ liệu theo thời gian trong bộ nhớ
            var filteredPracticeTestList = practiceTestList
                                            .Where(x => DateTime.ParseExact(x.CreatedDate, "MM/dd/yyyy HH:mm:ss", null) >= startTime)
                                            .ToList();

            // Tổng số bài test đã làm
            var totalTakenTest = filteredPracticeTestList
                                        .Select(x => x.TestId)
                                        .Distinct()
                                        .Count();

            // Số lần làm bài - tính cả làm lại 1 bài test
            var totalPracticeTimes = filteredPracticeTestList.Count();

            // Lần làm bài gần nhất
            var lastestPracticeTime = filteredPracticeTestList
                                        .OrderByDescending(x => x.Id)
                                        .FirstOrDefault();

            // Tổng thời gian làm
            var totalTime = filteredPracticeTestList
                                .Select(x => x.Time)
                                .Sum();

            // <% đúng của từng bài luyện> <result / totalPoint>
            List<ChartDto> correctPercentage = new List<ChartDto>();

            foreach (var practiceTest in filteredPracticeTestList)
            {
                var test = await _testRepository.GetByIdAsync(practiceTest.TestId);
                DateTime date;
                var formattedDate = "";
                if (DateTime.TryParseExact(practiceTest.CreatedDate, "MM/dd/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    formattedDate = date.ToString("dd/MM/yyyy");
                }

                var numberOfCorrectAnswers = _questionDbContext.AnswerSheet
                                            .Where(x => x.IsCorrect && x.PracticeTestId == practiceTest.Id)
                                            .Count();

                var item = (double)numberOfCorrectAnswers / test.NumberOfQuestions;

                correctPercentage.Add(new ChartDto
                {
                    Label = formattedDate,
                    Time = practiceTest.Time,
                    Quantity = Math.Round(item * 100, 2)
                });
            }

            // Tỉ lệ đúng theo chủ đề
            var correctRatesByTopicAndUser = new List<CorrectRateByTopicAndUserDto>();

            List<Topic> topics = (from topic in _questionDbContext.Topics
                                  join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
                                  join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
                                  join practiceTest in _questionDbContext.PracticeTest on answerSheet.PracticeTestId equals practiceTest.Id
                                  where practiceTest.UserId == userId
                                  select topic)
                                  .Distinct()
                                  .ToList();

            foreach (var topicElement in topics)
            {
                var totalQuestionsByTopic =
                                (from topic in _questionDbContext.Topics
                                 join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
                                 join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
                                 join practiceTest in _questionDbContext.PracticeTest on answerSheet.PracticeTestId equals practiceTest.Id
                                 where practiceTest.UserId == userId && topic.Id == topicElement.Id
                                 select topic)
                                 .Count();

                var correctQuestionByTopic =
                                (from topic in _questionDbContext.Topics
                                 join question in _questionDbContext.Questions on topic.Id equals question.ChuDeId
                                 join answerSheet in _questionDbContext.AnswerSheet on question.Id equals answerSheet.QuesitonId
                                 join practiceTest in _questionDbContext.PracticeTest on answerSheet.PracticeTestId equals practiceTest.Id
                                 where practiceTest.UserId == userId && topic.Id == topicElement.Id && answerSheet.IsCorrect
                                 select topic)
                                 .Count();

                if (totalQuestionsByTopic == 0)
                {
                    totalQuestionsByTopic = 1;
                }

                correctRatesByTopicAndUser.Add(new CorrectRateByTopicAndUserDto()
                {
                    TopicId = topicElement.Id,
                    TopicName = topicElement.Name,
                    CorrectAnswers = correctQuestionByTopic,
                    TotalAnswers = totalQuestionsByTopic,
                    CorrectRate = Math.Round((double)correctQuestionByTopic / totalQuestionsByTopic * 100, 2)
                });
            }

            // Thời gian trung bình
            var averageTime = Math.Round((double)correctPercentage.Sum(x => x.Time) / correctPercentage.Count(), 2);

            // Độ chính xác trung bình
            var averageCorrecPercent = Math.Round((double)correctPercentage.Sum(x => x.Quantity) / correctPercentage.Count(), 2);

            var totalPracticeTests = correctPercentage.Count();

            // Return res
            var res = new StatisticDto()
            {
                TotalTakenTest = totalTakenTest,
                TotalPracticeTime = totalTime,
                AverageCorrecPercent = averageCorrecPercent,
                AverageTime = averageTime,
                TotalPracticeTestTaken = totalPracticeTests,
                CorrectPercentage = correctPercentage,
                CorrectPercentageByTopicAndUser = correctRatesByTopicAndUser,
            };

            return res;
        }

        //public async Task<StatisticDto> GetStatisticByUser(int userId, int time)
        //{
        //    // Tính toán thời gian bắt đầu dựa trên tham số `time`
        //    DateTime startTime = DateTime.Now.AddDays(-time); // Nếu time là số ngày
        //                                                      // Nếu time là số tuần: DateTime startTime = DateTime.Now.AddDays(-time * 7);
        //                                                      // Nếu time là số tháng: DateTime startTime = DateTime.Now.AddMonths(-time);

        //    // Lấy toàn bộ dữ liệu PracticeTest vào bộ nhớ
        //    var practiceTestList = await _questionDbContext.PracticeTest
        //                                .Where(x => x.UserId == userId)
        //                                .ToListAsync();

        //    // Lọc dữ liệu theo thời gian trong bộ nhớ
        //    var filteredPracticeTestList = practiceTestList
        //                                    .Where(x => DateTime.ParseExact(x.CreatedDate, "MM/dd/yyyy HH:mm:ss", null) >= startTime)
        //                                    .ToList();

        //    // Tổng số bài test đã làm
        //    var totalTakenTest = filteredPracticeTestList
        //                                .Select(x => x.TestId)
        //                                .Distinct()
        //                                .Count();

        //    // Số lần làm bài - tính cả làm lại 1 bài test
        //    var totalPracticeTimes = filteredPracticeTestList.Count();

        //    // Lần làm bài gần nhất
        //    var lastestPracticeTime = filteredPracticeTestList
        //                                .OrderByDescending(x => x.Id)
        //                                .FirstOrDefault();

        //    // Tổng thời gian làm
        //    var totalTime = filteredPracticeTestList
        //                        .Select(x => x.Time)
        //                        .Sum();

        //    // <% đúng của từng bài luyện> <result / totalPoint>
        //    List<ChartDto> correctPercentage = new List<ChartDto>();

        //    foreach (var practiceTest in filteredPracticeTestList)
        //    {
        //        var test = await _testRepository.GetByIdAsync(practiceTest.TestId);
        //        DateTime date;
        //        var formattedDate = "";
        //        if (DateTime.TryParseExact(practiceTest.CreatedDate, "MM/dd/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out date))
        //        {
        //            formattedDate = date.ToString("dd/MM/yyyy");
        //        }

        //        var numberOfCorrectAnswers = await _questionDbContext.AnswerSheet
        //                                    .Where(x => x.IsCorrect && x.PracticeTestId == practiceTest.Id)
        //                                    .CountAsync();

        //        var item = (double)numberOfCorrectAnswers / test.NumberOfQuestions;

        //        correctPercentage.Add(new ChartDto
        //        {
        //            Label = formattedDate,
        //            Time = practiceTest.Time,
        //            Quantity = Math.Round(item * 100, 2)
        //        });
        //    }

        //    // Tỉ lệ đúng theo chủ đề
        //    var correctRatesByTopicAndUser = new List<CorrectRateByTopicAndUserDto>();

        //    // Lấy tất cả dữ liệu cần thiết vào bộ nhớ trước khi lọc
        //    var answerSheets = await _questionDbContext.AnswerSheet
        //                            .ToListAsync();

        //    var questions = await _questionDbContext.Questions
        //                            .ToListAsync();

        //    var topics = await _questionDbContext.Topics
        //                            .ToListAsync();

        //    foreach (var topicElement in topics)
        //    {
        //        var totalQuestionsByTopic = (from question in questions
        //                                     join answerSheet in answerSheets on question.Id equals answerSheet.QuesitonId
        //                                     join practiceTest in filteredPracticeTestList on answerSheet.PracticeTestId equals practiceTest.Id
        //                                     where question.ChuDeId == topicElement.Id
        //                                     select question)
        //                                     .Count();

        //        var correctQuestionByTopic = (from question in questions
        //                                      join answerSheet in answerSheets on question.Id equals answerSheet.QuesitonId
        //                                      join practiceTest in filteredPracticeTestList on answerSheet.PracticeTestId equals practiceTest.Id
        //                                      where question.ChuDeId == topicElement.Id && answerSheet.IsCorrect
        //                                      select question)
        //                                      .Count();

        //        if (totalQuestionsByTopic == 0)
        //        {
        //            totalQuestionsByTopic = 1;
        //        }

        //        correctRatesByTopicAndUser.Add(new CorrectRateByTopicAndUserDto()
        //        {
        //            TopicId = topicElement.Id,
        //            TopicName = topicElement.Name,
        //            CorrectAnswers = correctQuestionByTopic,
        //            TotalAnswers = totalQuestionsByTopic,
        //            CorrectRate = Math.Round((double)correctQuestionByTopic / totalQuestionsByTopic * 100, 2)
        //        });
        //    }

        //    // Thời gian trung bình
        //    var averageTime = Math.Round((double)correctPercentage.Sum(x => x.Time) / correctPercentage.Count(), 2);

        //    // Độ chính xác trung bình
        //    var averageCorrecPercent = Math.Round((double)correctPercentage.Sum(x => x.Quantity) / correctPercentage.Count(), 2);

        //    var totalPracticeTests = correctPercentage.Count();

        //    // Return res
        //    var res = new StatisticDto()
        //    {
        //        TotalTakenTest = totalTakenTest,
        //        TotalPracticeTime = totalTime,
        //        AverageCorrecPercent = averageCorrecPercent,
        //        AverageTime = averageTime,
        //        TotalPracticeTestTaken = totalPracticeTests,
        //        CorrectPercentage = correctPercentage,
        //        CorrectPercentageByTopicAndUser = correctRatesByTopicAndUser,
        //    };

        //    return res;
        //}

    }
}
