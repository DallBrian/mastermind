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
                    gameUniqueColorCountMap[c.Key] = count++;
                }
                else
                {
                    gameUniqueColorCountMap.Add(c.Key, 1);
                }
            });

            var colorMatchCount = 0;
            for (var i = 0; i < guess.ColorCode.Count; i++)
            {
                var key = guess.ColorCode[i].Key;
                var isFullMatch = gameCode.ColorCode[i].Key.Equals(key);
                if (isFullMatch)
                {
                    gameUniqueColorCountMap[key]--;
                    continue;
                }

                if (!gameUniqueColorCountMap.TryGetValue(key, out var count)) continue;
                if (count <= 0) continue;

                gameUniqueColorCountMap[key]--;
                colorMatchCount++;
            }

            return colorMatchCount;
        }

        public CodeEntry Guess { get; }
        public int ColorMatch { get; }
        public int FullMatch { get; }

        public override string ToString()
        {
            return $"{FullMatch}/{ColorMatch} {Guess}";
        }
    }
}