using datn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<StatisticDto> GetStatisticByUser(int userId, int time)
        {
            /// Thiếu lọc theo thời gian 
            /// và lấy tỉ lệ đúng các dạng câu hỏi
            /// 

            // Tổng số bài test đã làm
            var totalTakenTest = _questionDbContext.PracticeTest
                                    .Where(x => x.UserId == userId)
                                    .Select(x => x.TestId)
                                    .Distinct()
                                    .Count();

            // Số lần làm bài - tính cả làm lại 1 bài test
            var totalPracticeTimes = _questionDbContext.PracticeTest
                                    .Where(x => x.UserId == userId)
                                    .Distinct()
                                    .Count();
            // Lần làm bài gần nhất
            var lastestPracticeTime = _questionDbContext.PracticeTest.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == userId);

            // % đúng trung bình
            double averageCorrecPercent = 0;

            // Thời gian trung bình hoàn thành bài test
            double averageTime = 0;

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

                if (totalQuestionsByTopic == 0)
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
