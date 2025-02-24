namespace Mastermind.Models
{
    public enum AppPhase
    {
        MainMenu,
        InGame,
        Exited
    }

    public class ApplicationState
    {
        public AppPhase Phase { get; set; } = AppPhase.MainMenu;

        public GameState? CurrentGame { get; set; }

        public GameState[] GameHistory { get; set; } = [];
    }
}
