using Mastermind.Display;
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
            return guess.ColorCode.Where((c, i) => c.Key.Equals(gameCode.ColorCode[i].Key)).Count();
        }

        private int GetColorMatches(CodeEntry guess, CodeEntry gameCode)
        {
            // Get a map of the colors and their counts within the game code
            var gameUniqueColorCountMap = new Dictionary<char, int>();
            gameCode.ColorCode.ForEach(c =>
            {
                if (gameUniqueColorCountMap.TryGetValue(c.Key, out var count))
                {
                    gameUniqueColorCountMap[c.Key] = count + 1;
                }
                else
                {
                    gameUniqueColorCountMap.Add(c.Key, 1);
                }
            });

            // Remove all guess keys and decrement color count for exact matches
            var guessColorCodesNotExactMatch = new List<ColorKey>();
            for (var i = 0; i < guess.ColorCode.Count; i++)
            {
                var key = guess.ColorCode[i].Key;
                var isFullMatch = gameCode.ColorCode[i].Key.Equals(key);
                if (isFullMatch)
                {
                    gameUniqueColorCountMap[key] -= 1;
                }
                else
                {
                    guessColorCodesNotExactMatch.Add(guess.ColorCode[i]);
                }
            }

            // If colors still exist in map for non full match guess keys then increment color count and decrement map
            var colorMatchCount = 0;
            foreach (var colorKey in guessColorCodesNotExactMatch)
            {
                var key = colorKey.Key;
                
                if (!gameUniqueColorCountMap.TryGetValue(key, out var count)) continue;
                if (count <= 0) continue;

                gameUniqueColorCountMap[key] -= 1;
                colorMatchCount++;
            }

            return colorMatchCount;
        }

        public CodeEntry Guess { get; }
        public int ColorMatch { get; }
        public int FullMatch { get; }

        public string ToDisplay()
        {
            return $"{new StyledString($"{FullMatch}/{ColorMatch}", center: .25).ToJson()}{new StyledString(" ", center: .10).ToJson()}{Guess.ToDisplay()}";
        }

        public override string ToString()
        {
            return $"{FullMatch}/{ColorMatch} {Guess}";
        }
    }
}