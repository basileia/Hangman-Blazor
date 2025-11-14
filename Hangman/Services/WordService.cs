using Hangman.Common;
using Hangman.Data.Models;
using Hangman.Interfaces;

namespace Hangman.Services
{
    public class WordService : IWordService
    {
        private readonly ILogger<WordService> _logger;
        private readonly IWordRepository _wordRepository;

        public WordService(
            ILogger<WordService> logger,
            IWordRepository wordRepository)
        {
            _logger = logger;
            _wordRepository = wordRepository;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _wordRepository.GetCategoriesAsync();
        }

        public async Task<Result<string, string>> GetRandomWordAsync(Category category)
        {
            try
            {
                var randomWord = await _wordRepository.GetRandomWordAsync(category);

                if (randomWord == null)
                {
                    _logger.LogWarning("No words found for category {Category}", category);
                    return Result<string, string>.Fail($"Nebylo nalezeno žádné slovo pro kategorii '{category}'");
                }

                return Result<string, string>.Ok(randomWord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving random word for category {Category}", category);
                return Result<string, string>.Fail("Nepodařilo se načíst náhodné slovo.");
            }
        }
    }
}
