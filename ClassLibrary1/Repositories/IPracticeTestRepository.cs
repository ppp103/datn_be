using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public interface IPracticeTestRepository 
    {
        Task<List<PracticeTestDto>> GetAllPracticeTest();
        Task<int> GetPracticeTestResult (List<AnswerSheetDto> answerSheet);
        Task<PracticeTestDto> GetPracticeTestById(int id);
        Task<PagedList<PracticeTestDto>> GetPracticeTestByTypeId(int id, int type, int page, int pageSize);
        Task<PracticeTestDto> CreateAsync(PracticeTestDto test);
        //Task<StatisticDto> GetStatisticByUser(int userId, int time);
    }
}
