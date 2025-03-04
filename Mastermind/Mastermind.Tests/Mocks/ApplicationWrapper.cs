using Mastermind.State;

namespace Mastermind.Tests.Mocks
{
    public class ApplicationWrapper
    {
        public ApplicationState State { get; }
        public Application App { get; }
        public MockView View { get; }

        public ApplicationWrapper(ApplicationState? appState = null)
        {
            State = appState ?? new ApplicationState();
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
