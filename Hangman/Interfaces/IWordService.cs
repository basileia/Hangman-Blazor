using Hangman.Common;

namespace Hangman.Interfaces
{
    public interface IWordService
    {
        List<string> GetCategories();
        string? GetRandomWord(string? category = null);
        public Result<int, string> LoadWords(string filePath);
    }
}
