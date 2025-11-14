using Hangman.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hangman.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<WordEntry> WordEntries => Set<WordEntry>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordEntry>()
                .Property(w => w.Category)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<WordEntry>().ToTable("Words");

            base.OnModelCreating(modelBuilder);
        }
    }
}
