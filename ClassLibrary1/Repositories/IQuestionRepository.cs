namespace datn.Domain
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllQuestionsAsync();

        Task<PagedList<QuestionDto>> GetAllQuestionPaggingAsync(int page, int pageSize, string keyWord, int? chuDeId, int? loaiCauId, int? difficultyLevel);

        Task<List<QuestionDto>> GetQuestionByTestIdAsync(int testId);

        Task<QuestionDto> GetByIdAsync(int id);

        Task<Question> CreateAsync(Question question);  

        Task<int> UpdateAsync(int id, Question question);  

        Task<int> DeleteAsync(int id); 
    }
}
