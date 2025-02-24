namespace Mastermind.Models
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
        public CodeEntry GameCode { get; set; }
        public List<Attempt> Attempts { get; set; } = [];
    }
}