using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public interface ITestRepository
    {
        Task<PagedList<TestDto>> GetAllTestPaggingAsync(int page, int pageSize);
        Task<Test> CreateAsync(Test test);

    }
}
