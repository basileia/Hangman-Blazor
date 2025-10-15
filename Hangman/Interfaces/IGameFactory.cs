using Hangman.Services;

namespace Hangman.Interfaces
{
    public interface IGameFactory
    {
        IHangmanGameService CreateGame(string word);
    }
}
