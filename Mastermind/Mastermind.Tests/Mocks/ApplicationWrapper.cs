using Mastermind.Models;

namespace Mastermind.Tests.Mocks
{
    public class ApplicationWrapper
    {
        public ApplicationState State { get; }
        public Application App { get; }
        public MockView View { get; }

        public ApplicationWrapper()
        {
            State = new ApplicationState();
            View = new MockView();
            App = new Application(View, State);
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
