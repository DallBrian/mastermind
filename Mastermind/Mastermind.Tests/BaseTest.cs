using Mastermind.State;
using Mastermind.Tests.Mocks;

[assembly: Parallelizable(ParallelScope.All)]

namespace Mastermind.Tests
{
    [Category("Regression")]
    public class BaseTest
    {
        public ApplicationWrapper Start(ApplicationState? appState = null)
        {
            var app = new ApplicationWrapper(appState);
            app.App.Run();
            app.View.WaitForPrintScreen();
            return app;
        }
    }
}