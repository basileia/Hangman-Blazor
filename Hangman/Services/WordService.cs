using Hangman.Common;
using Hangman.Interfaces;
using Hangman.Models;

namespace Hangman.Services
{
    public class WordService : IWordService
    {
        private readonly ILogger<WordService> _logger;
        private readonly ICsvLoader _csvLoader;
        private IReadOnlyList<WordEntry> _words = Array.Empty<WordEntry>();
        private readonly object _lock = new();

        public WordService(
            ILogger<WordService> logger,
            ICsvLoader csvLoader)
        {
            _logger = logger;
            _csvLoader = csvLoader;
        }


        public Result<int, string> LoadWords(string filePath)
        {
            var result = _csvLoader.Load(filePath);

            if (!result.IsSuccess)
            {
                return Result<int, string>.Fail(result.Error!);
            }

            lock (_lock)
            {
                _words = result.Value!;
            }

            return Result<int, string>.Ok(_words.Count);
        }

        public List<string> GetCategories()
        {
            lock (_lock) 
            { 
                return _words.Select(w => w.Category).Distinct().ToList();
            }
        }

        public string? GetRandomWord(string? category = null)
        {
            var query = string.IsNullOrEmpty(category)
                ? _words
                : _words.Where(w => w.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!query.Any())
            {
                _logger.LogWarning("No words were found for category '{Category}'.", category);
                return null;
            }

            var index = Random.Shared.Next(query.Count);
            return query[index].Word;
        }
    }
}
