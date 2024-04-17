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
        Task<PracticeTestDto> GetPracticeTestById(int id);
        Task<PracticeTestDto> CreateAsync(PracticeTestDto test);
    }
}
