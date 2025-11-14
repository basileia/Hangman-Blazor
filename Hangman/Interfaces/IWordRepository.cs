using Hangman.Data.Models;

namespace Hangman.Interfaces
{
    public interface IWordRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<string?> GetRandomWordAsync(Category category);
    }
}
