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
        public void CanStopNewGame()
        {
            var app = Start(new ApplicationState() {Phase = AppPhase.InGame});
            app.SimulateUserInput("quit");
 
        }
    }
}
