using System.ComponentModel.DataAnnotations;

namespace Hangman.Data.Models
{
    public class WordEntry
    {
        [Key]
        public int Id { get; set; }

        public Category Category { get; set; }

        [MaxLength(200)]
        public required string Word { get; set; }
    }
}
