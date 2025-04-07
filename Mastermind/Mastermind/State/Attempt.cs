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
            return guess.ColorCode.Where((colorKey, i) => colorKey.Key.Equals(gameCode.ColorCode[i].Key)).Count();
        }

        private int GetColorMatches(CodeEntry guess, CodeEntry gameCode)
        {
            var nonMatchingIndices = new List<int>();
            for (var i = 0; i < guess.ColorCode.Count; i++)
            {
                var guessChar = guess.ColorCode[i].Key;
                var codeChar = gameCode.ColorCode[i].Key;
                if (!guessChar.Equals(codeChar)) nonMatchingIndices.Add(i);
            }

            var codeColors = gameCode.ColorCode
                .Where((_, i) => nonMatchingIndices.Contains(i))
                .Select(c => c.Key).ToList();
            var guessColors = guess.ColorCode
                .Where((_, i) => nonMatchingIndices.Contains(i))
                .Select(c => c.Key).ToList();
            var intersect = codeColors.Intersect(guessColors).ToList();
            return intersect.Count;
        }

        public CodeEntry Guess { get; }
        public int ColorMatch { get; }
        public int FullMatch { get; }
    }
}