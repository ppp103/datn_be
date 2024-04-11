namespace datn.Domain

{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllQuestionsAsync();

        Task<PagedList<Question>> GetAllQuestionPaggingAsync(int page, int pageSize, string keyWord);

        //Task<List<Question>> GetByIdAsync(int id); 

        Task<Question> GetByIdAsync(int id);

        Task<Question> CreateAsync(Question question);  

        Task<int> UpdateAsync(int id, Question question);  

        Task<int> DeleteAsync(int id); 

    }
}
