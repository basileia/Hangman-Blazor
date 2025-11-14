using Hangman.Data.Models;
using Hangman.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hangman.Data.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly AppDbContext _context;

        public WordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.WordEntries
                                 .Select(w => w.Category)
                                 .Distinct()
                                 .OrderBy(c => c)
                                 .ToListAsync();
        }

        public async Task<string?> GetRandomWordAsync(Category category)
        {
            return await _context.WordEntries
                                 .Where(w => w.Category == category)
                                 .OrderBy(w => EF.Functions.Random())
                                 .Select(w => w.Word)
                                 .FirstOrDefaultAsync();
        }
    }
}
