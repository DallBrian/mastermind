using System.Text;
using Mastermind.State;

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
            var sb = new StringBuilder();
            sb.AppendLine("Mastermind");

            switch (state.Phase)
            {
                case AppPhase.MainMenu:
                    sb.AppendLine("1 New Game");
                    break;
                case AppPhase.InGame:
                    sb.Append(GetGameView(state.CurrentGame!));
                    break;
            }

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
