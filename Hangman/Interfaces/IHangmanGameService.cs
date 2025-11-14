using Hangman.Common;

namespace Hangman.Interfaces
{
    public interface IHangmanGameService
    {
        string SecretWord { get; }
        HashSet<char> GuessedLetters { get; }
        int NumberOfMistakes { get; }
        int MaxMistakes { get; }

        Result<bool, string> Guess(char letter);
        string GetCurrentProgress();
        bool IsGameOver();
        bool IsWordGuessed();
        void ForceWin();
        void ForceLose();
    }
}
