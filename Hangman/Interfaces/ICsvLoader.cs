using Hangman.Common;
using Hangman.Models;

namespace Hangman.Interfaces
{
    public interface ICsvLoader
    {
        Result<IReadOnlyList<WordEntry>, string> Load(string filePath);
    }
}
