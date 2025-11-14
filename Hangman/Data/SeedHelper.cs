using Hangman.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hangman.Data
{
    public static class SeedHelper
    {
        public static async Task SeedDataIfEmptyAsync(AppDbContext db, ILogger logger)
        {
            if (await db.WordEntries.AnyAsync())
            {
                logger.LogInformation("Database already contains data, skipping seeding.");
                return;
            }

            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "words.csv");

            if (!File.Exists(filePath))
            {
                logger.LogWarning("CSV file not found: {FilePath}", filePath);
                return;
            }

            string[] lines;
            try
            {
                lines = await File.ReadAllLinesAsync(filePath);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error reading CSV file: {filePath}");
                return;
            }

            var words = new List<WordEntry>();
            int lineNumber = 0;

            foreach (var line in lines.Skip(1)) 
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',', StringSplitOptions.TrimEntries);
                if (parts.Length < 2)
                {
                    logger.LogWarning($"Line {lineNumber} has insufficient columns: '{line}'");
                    continue;
                }

                if (!Enum.TryParse<Category>(parts[0], true, out var category))
                {
                    logger.LogWarning($"Invalid category on line {lineNumber}: '{parts[0]}'");
                    continue;
                }

                var word = parts[1];
                if (string.IsNullOrWhiteSpace(word))
                {
                    logger.LogWarning($"Empty word on line {lineNumber}");
                    continue;
                }

                words.Add(new WordEntry
                {
                    Category = category,
                    Word = word
                });
            }

            if (words.Count > 0)
            {
                await db.WordEntries.AddRangeAsync(words);
                await db.SaveChangesAsync();
                logger.LogInformation($"Seeding completed, {words.Count} words added.");
            }
            else
            {
                logger.LogWarning("No valid data found to seed.");
            }
        }
    }
}
