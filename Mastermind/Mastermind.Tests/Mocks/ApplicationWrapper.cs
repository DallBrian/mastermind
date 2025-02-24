namespace Mastermind.Tests.Mocks
{
    public class ApplicationWrapper
    {
        public Application App { get; }
        public MockView View { get; }

        public ApplicationWrapper()
        {
            View = new MockView();
            App = new Application(View);
        }

        public void SimulateUserInput(ConsoleKey key)
        {
            View.SendResponse(key);
        }

        public void SimulateUserInput(string userInput)
        {
            View.SendResponse(userInput);
        }
    }
}
