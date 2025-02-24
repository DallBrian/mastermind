using Mastermind.Models;

namespace Mastermind.Display
{
    /// <summary>
    /// This interface is what's used in the application so we can inject an object that doesn't use the Console for testing
    /// </summary>
    public interface IView
    {
        public string GetResponse();
        public ConsoleKeyInfo GetKeyResponse();
        public void PrintScreen(ApplicationState state);
    }
}
