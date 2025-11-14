using Hangman.Interfaces;

namespace Hangman.Services
{
    public class GameFactory : IGameFactory
    {
        public IHangmanGameService CreateGame(string word)
        {
            return new HangmanGameService(word);
        }
    }
}
