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
            //TODO make a psedo random code
            return new CodeEntry("rbyg");
        }
    }
}