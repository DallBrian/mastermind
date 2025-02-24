namespace Mastermind.Tests
{
    public class ApplicationTests : BaseTest
    {
        [Test]
        public void ApplicationMainMenuDisplays()
        {
            var app = Start();
            Assert.That(app.View.Text, Is.EqualTo(
                "Mastermind" + Environment.NewLine +
                "1 New Game" + Environment.NewLine));
        }

        [Test]
        public void ApplicationExitsOnKeyPress()
        {
            var app = Start();
            Assert.That(app.App.IsRunning, Is.True);
            app.SimulateUserInput(ConsoleKey.Escape);
            Assert.That(app.App.IsRunning, Is.False);
        }
    }
}
