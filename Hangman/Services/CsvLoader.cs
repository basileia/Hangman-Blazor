using Hangman.Common;
using Hangman.Interfaces;
using Hangman.Models;

namespace Hangman.Services
{
    public class CsvLoader : ICsvLoader
    {
        private readonly ILogger<CsvLoader> _logger;

        public CsvLoader(ILogger<CsvLoader> logger)
        {
            _logger = logger;
        }

        public Result<IReadOnlyList<WordEntry>, string> Load(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return Result<IReadOnlyList<WordEntry>, string>.Fail(
                    "Cesta k souboru nesmí být prázdná.");
            }

            if (!File.Exists(filePath))
            {
                _logger.LogWarning("CSV file not found: {FilePath}", filePath);
                return Result<IReadOnlyList<WordEntry>, string>.Fail(
                    $"Soubor '{filePath}' nebyl nalezen.");
            }

            try
            {
                var words = ParseCsvFile(filePath);

                if (words.Count == 0)
                {
                    _logger.LogWarning("CSV file '{FilePath}' does not contain any valid words.", filePath);
                    return Result<IReadOnlyList<WordEntry>, string>.Fail(
                        "Soubor neobsahuje žádná platná slova.");
                }

                _logger.LogInformation("Successfully loaded {Count} words from {FilePath}",
                    words.Count, filePath);

                return Result<IReadOnlyList<WordEntry>, string>.Ok(words);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "I/O error loading CSV: {FilePath}", filePath);
                return Result<IReadOnlyList<WordEntry>, string>.Fail(
                    $"Chyba při načítání souboru: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Access denied to CSV: {FilePath}", filePath);
                return Result<IReadOnlyList<WordEntry>, string>.Fail(
                    "Přístup k souboru byl odepřen.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while loading CSV: {FilePath}", filePath);
                throw;
            }
        }

        private List<WordEntry> ParseCsvFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            if (lines.Length <= 1)
            {
                return new List<WordEntry>();
            }

            var words = new List<WordEntry>();

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');

                if (parts.Length >= 2 && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    words.Add(new WordEntry(parts[0].Trim(), parts[1].Trim().ToUpperInvariant()));
                }
                else
                {
                    Console.WriteLine($"[Warning] Ignoruji řádek se špatným formátem: {line}");
                }
            }
            return words;
        }        
    }
}
