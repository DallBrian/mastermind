namespace Mastermind.State
{
    public enum GameResult
    {
        Pending,
        Won,
        Lost,
        Quit
    }

    public class GameState
    {
        public GameResult Result { get; set; } = GameResult.Pending;
        public CodeEntry GameCode { get; set; } = GenerateGameCode();
        public List<Attempt> Attempts { get; set; } = [];

        private static CodeEntry GenerateGameCode()
        {
            var _maxLength = 4;
            var _validChars = new[] { 'r', 'b', 'y', 'g' };
            var rand = new Random();
            var random = string.Concat(rand.GetItems(_validChars, _maxLength));
            return new CodeEntry(random);
        }
    }
}