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
            var rand = new Random();
            var random = string.Concat(rand.GetItems(GameOptions.ValidChars.ToArray(), GameOptions.CodeLength));
            return new CodeEntry(random);
        }
    }
}