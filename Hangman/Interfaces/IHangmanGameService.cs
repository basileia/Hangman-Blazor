namespace Hangman.Interfaces
{
    public interface IHangmanGameService
    {
        (bool Success, string Message) Guess(char letter);
        string GetCurrentProgress();
        bool IsGameOver();
        bool IsWordGuessed();
        void ForceWin();
        void ForceLose();
    }
}
