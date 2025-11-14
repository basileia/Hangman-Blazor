using Hangman.Common;
using Hangman.Data.Models;

namespace Hangman.Interfaces
{
    public interface IWordService
    {
        Task<Result<string, string>> GetRandomWordAsync(Category category);
        Task<List<Category>> GetCategoriesAsync();
    }
}
