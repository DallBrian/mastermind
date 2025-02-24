using Mastermind.Models;
using System.Text;

namespace Mastermind.Display
{
    /// <summary>
    /// Controls the view and user input within the application using virtual methods for anything that accesses the
    /// Console so that can be overriden in the MockView for testing.
    /// </summary>
    public class View : IView
    {
        public virtual string GetResponse()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public virtual ConsoleKeyInfo GetKeyResponse()
        {
            return Console.ReadKey();
        }

        public virtual void PrintScreen(ApplicationState state)
        {
            Console.Clear();
            Console.Write(GetStringView(state));
        }

        public string GetStringView(ApplicationState state)
        {
            var stringView = new StringBuilder();
            stringView.AppendLine("Mastermind");
            
            switch (state.Phase)
            {
                case AppPhase.MainMenu:
                    stringView.Append(GetMainMenuView(state));
                    break;
                case AppPhase.InGame:
                    stringView.Append(GetGameView(state.CurrentGame!));
                    break;
            }

            return stringView.ToString();
        }

        private string GetMainMenuView(ApplicationState state)
        {
            var sb = new StringBuilder();
            sb.AppendLine("1 New Game");

            return sb.ToString();
        }

        private string GetGameView(GameState gameState)
        {
            var sb = new StringBuilder();
            var attempts = new List<Attempt>();
            attempts.AddRange(gameState.Attempts);
            attempts.Reverse();
            foreach (var attempt in attempts)
            {
                sb.AppendLine(attempt.ToString());
            }

            return sb.ToString();
        }
    }
}
