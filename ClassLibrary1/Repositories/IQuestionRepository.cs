using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllQuestionsAsync();

        //Task<List<Question>> GetByIdAsync(int id); 

        Task<Question> GetByIdAsync(int id);

        Task<Question> CreateAsync(Question question);  

        Task<int> UpdateAsync(int id, Question question);  

        Task<int> DeleteAsync(int id); 

    }
}
