using Hangman.Common;
using Hangman.Interfaces;

namespace Hangman.Services
{
    public class HangmanGameService : IHangmanGameService

    {
        public string SecretWord { get; }
        public HashSet<char> GuessedLetters { get; } = new();
        public int NumberOfMistakes { get; private set; } = 0;
        public int MaxMistakes { get; }

        public HangmanGameService(string secretWord, int maxMistakes = 6)
        {
            SecretWord = secretWord.ToUpper();
            MaxMistakes = maxMistakes;
        }

        public Result<bool, string> Guess(char letter)
        {
            letter = char.ToUpper(letter);

            if (!char.IsLetter(letter))
                return Result<bool, string>.Fail("Zadejte platné písmeno.");

            if (GuessedLetters.Contains(letter))
                return Result<bool, string>.Fail("Toto písmeno jsi už zkoušel.");

            GuessedLetters.Add(letter);

            bool isCorrect = SecretWord.Contains(letter);
            if (!isCorrect)
            {
                NumberOfMistakes++;
                return Result<bool, string>.Fail($"Písmeno '{letter}' ve slově není.");
            }
            return Result<bool, string>.Ok(isCorrect);
        }        

        public string GetCurrentProgress()
        {
            return string.Concat(SecretWord.Select(c =>
        char.IsLetter(c) ? (GuessedLetters.Contains(c) ? c : '_') : c
    ));
        }

        public bool IsGameOver()
        {
            return NumberOfMistakes >= MaxMistakes || IsWordGuessed();
        }

        public bool IsWordGuessed()
        {
            return SecretWord.All(c => GuessedLetters.Contains(c));
        }

        public void ForceWin()
        {
            NumberOfMistakes = 0;
            foreach (var c in SecretWord.Distinct())
                GuessedLetters.Add(c);
        }

        public void ForceLose()
        {
            NumberOfMistakes = MaxMistakes;
        }
    }
}
