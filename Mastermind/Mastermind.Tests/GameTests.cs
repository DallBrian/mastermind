using Mastermind.State;

namespace Mastermind.Tests
{
    public class GameTests : BaseTest
    {
        [Test]
        public void CanStartNewGame()
        {
            var app = Start();
            Assert.That(app.State.Phase, Is.EqualTo(AppPhase.MainMenu));
            app.SimulateUserInput(ConsoleKey.D1);
            Assert.That(app.State.Phase, Is.EqualTo(AppPhase.InGame));
        }

        [Test]
        public void CanMakeValidAttempt()
        {
            var gameCode = new CodeEntry("rbyg");
            var initialState = new ApplicationState
            {
                Phase = AppPhase.InGame,
                CurrentGame = new GameState
                {
                    Result = GameResult.Pending,
                    GameCode = gameCode,
                }
            };
            var app = Start(initialState);
            app.SimulateUserInput("rybr");
            Assert.That(app.State.CurrentGame.Attempts, Has.Count.EqualTo(1));
        }

        [Test]
        public void CanWinGame()
        {
            var gameCode = new CodeEntry("rbyg");
            var initialState = new ApplicationState
            {
                Phase = AppPhase.InGame,
                CurrentGame = new GameState
                {
                    Result = GameResult.Pending,
                    GameCode = gameCode,
                }
            };
            var app = Start(initialState);
            app.SimulateUserInput("rbyg");
            Assert.That(app.State.CurrentGame.Result, Is.EqualTo(GameResult.Won));
        }

        [Test]
        public void GoBackToMainMenuAfterGameAndRecordHistory()
        {
            var initialState = new ApplicationState
            {
                Phase = AppPhase.InGame,
                CurrentGame = new GameState
                {
                    Result = GameResult.Won
                }
            };
            var app = Start(initialState);
            app.SimulateUserInput("");
            Assert.That(app.State.Phase, Is.EqualTo(AppPhase.MainMenu));
            Assert.That(app.State.GameHistory, Has.Count.EqualTo(1));
        }
    }
}
