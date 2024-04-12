using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public interface IQuestionCategoryRepository
    {
        Task<List<QuestionCategory>> GetAllQuestionCategoryAsync();

    }
}
