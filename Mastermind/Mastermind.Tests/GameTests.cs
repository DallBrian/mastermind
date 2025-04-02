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
            var app = Start(new ApplicationState() { Phase = AppPhase.InGame });
            app.SimulateUserInput("quit");
            Assert.That(app.State.Phase, Is.EqualTo(AppPhase.MainMenu));

        }

        public static TestCaseData[] CalculateAttemptTestCases = [
            new TestCaseData(new CodeEntry("rrrr"), new CodeEntry("rbrb"), 0, 2),
            ];

        [TestCaseSource(nameof(CalculateAttemptTestCases))]
        public void CalculateAttempts(CodeEntry guess, CodeEntry gamecode, int expectedColorMatch, int expectedFullMatch)
        {
            var attempt = new Attempt(guess, gamecode);
            Assert.That(attempt.ColorMatch, Is.EqualTo(expectedColorMatch));
            Assert.That(attempt.FullMatch, Is.EqualTo(expectedFullMatch));
        }
    }
}
