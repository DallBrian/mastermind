using Mastermind.Exceptions;

namespace Mastermind.State
{
    public class Attempt
    {
        public Attempt(CodeEntry guess, CodeEntry gameCode)
        {
            if (!guess.IsValid) throw new InvalidCodeEntryException();
            Guess = guess;
            ColorMatch = GetColorMatches(guess, gameCode);
            FullMatch = GetFullMatches(guess, gameCode);
        }

        private int GetFullMatches(CodeEntry guess, CodeEntry gameCode)
        {
            throw new NotImplementedException();
        }

        private int GetColorMatches(CodeEntry guess, CodeEntry gameCode)
        {
            throw new NotImplementedException();
        }

        public CodeEntry Guess { get; }
        public int ColorMatch { get; }
        public int FullMatch { get; }
    }
}