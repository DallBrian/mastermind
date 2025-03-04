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
            var stringView = GetStringView(state);
            var newLines = stringView.Split(Environment.NewLine);
            foreach (var line in newLines)
            {
                var parsedStrings = line.Split(";", StringSplitOptions.RemoveEmptyEntries);
                foreach (var parsedString in parsedStrings)
                {
                    var styledString = StyledString.FromJson(parsedString);
                    if (styledString.ForegroundColor is not null)
                        Console.ForegroundColor = styledString.ForegroundColor.Value;
                    if (styledString.BackgroundColor is not null)
                        Console.BackgroundColor = styledString.BackgroundColor.Value;
                    if (styledString.CenterPosition is not null)
                    {
                        var width = (int)Math.Round(_maxWidth * styledString.CenterPosition.Value, 0, MidpointRounding.ToZero);
                        styledString.Value = styledString.Value
                            .PadLeft(((width - styledString.Value.Length) / 2) + styledString.Value.Length)
                            .PadRight(width);
                    }

                    Console.Write(styledString.Value);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        private static readonly string _asciiArtTitle = "\r\n" +
                                                 "___  ___          _                      _           _ \r\n" +
                                                 "|  \\/  |         | |                    (_)         | |\r\n" +
                                                 "| .  . | __ _ ___| |_ ___ _ __ _ __ ___  _ _ __   __| |\r\n" +
                                                 "| |\\/| |/ _` / __| __/ _ \\ '__| '_ ` _ \\| | '_ \\ / _` |\r\n" +
                                                 "| |  | | (_| \\__ \\ ||  __/ |  | | | | | | | | | | (_| |\r\n" +
                                                 "\\_|  |_/\\__,_|___/\\__\\___|_|  |_| |_| |_|_|_| |_|\\__,_|\r\n" +
                                                 "                                                       ";

        private int _maxWidth = _asciiArtTitle.Split(Environment.NewLine).Select(l => l.Length).Max();

        public string GetStringView(ApplicationState state)
        {
            var sb = new StringBuilder();
            var splitNewLines = _asciiArtTitle.Split(Environment.NewLine);
            foreach (var line in splitNewLines)
            {
               sb.AppendLine(new StyledString(line, ConsoleColor.Black, ConsoleColor.White).ToJson());
            }

            foreach (var validChar in GameOptions.ValidChars)
            {
                sb.Append(new ColorKey(validChar).ToDisplay(ConsoleColor.Black, 1));
            }
            sb.AppendLine();

            switch (state.Phase)
            {
                case AppPhase.MainMenu:
                    sb.AppendLine(new StyledString("1 New Game", center: 1).ToJson());
                    sb.AppendLine(new StyledString("2 Options", center: 1).ToJson());
                    break;
                case AppPhase.Options:
                    sb.AppendLine(new StyledString($"{nameof(GameOptions.CodeLength)}: {GameOptions.CodeLength}",
                        center: 1).ToJson());
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
            foreach (var attempt in gameState.Attempts)
            {
                sb.AppendLine(attempt.ToDisplay());
            }

            if (gameState.Result == GameResult.Won)
            {
                sb.AppendLine(new StyledString("You Won!", center: 1).ToJson());
            }

            return sb.ToString();
        }
    }
}
