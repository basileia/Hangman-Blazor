namespace Hangman.Interfaces
{
    public interface IHangmanGameService
    {
        string SecretWord { get; }
        HashSet<char> GuessedLetters { get; }
        int NumberOfMistakes { get; }
        int MaxMistakes { get; }

        (bool Success, string Message) Guess(char letter);
        string GetCurrentProgress();
        bool IsGameOver();
        bool IsWordGuessed();
        void ForceWin();
        void ForceLose();
    }
}
