using Mastermind.Tests.Mocks;

[assembly: Parallelizable(ParallelScope.All)]

namespace Mastermind.Tests
{
    [Category("Regression")]
    public class BaseTest
    {
        public ApplicationWrapper Start()
        {
            var app = new ApplicationWrapper();
            app.App.Run();
            app.View.WaitForPrintScreen();
            return app;
        }
    }
}