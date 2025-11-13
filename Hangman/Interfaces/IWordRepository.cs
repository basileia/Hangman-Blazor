using Hangman.Data.Models;

namespace Hangman.Interfaces
{
    public interface IWordRepository
    {
        Task<List<WordEntry>> GetAllWordsAsync();
        Task<List<WordEntry>> GetWordsByCategoryAsync(Category category);
        Task<List<Category>> GetCategoriesAsync();
        Task<string?> GetRandomWordAsync(Category category);
    }
}
