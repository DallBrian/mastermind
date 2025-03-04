using Mastermind.Display;

namespace Mastermind.State
{
    public class CodeEntry
    {
        public CodeEntry(string codeString)
        {
            ColorCode = codeString.ToCharArray().Select(c => new ColorKey(c)).ToList();
        }

        public List<ColorKey> ColorCode { get; }

        public bool IsValid => ColorCode.Count == GameOptions.CodeLength && ColorCode.All(c => c.IsValid);

        public string ToDisplay()
        {
            return string.Concat(ColorCode.Select(c => c.ToDisplay()));
        }

        public override string ToString()
        {
            return string.Concat(ColorCode);
        }
    }

    public class ColorKey(char key)
    {
        public char Key { get; set; } = key;

        private ConsoleColor Foreground = key switch
        {
            'r' => ConsoleColor.Red,
            'b' => ConsoleColor.Blue,
            'y' => ConsoleColor.Yellow,
            'g' => ConsoleColor.Green,
            'c' => ConsoleColor.Cyan,
            'm' => ConsoleColor.Magenta
        };

        private ConsoleColor Background = key switch
        {
            'r' => ConsoleColor.Red,
            'b' => ConsoleColor.Blue,
            'y' => ConsoleColor.Yellow,
            'g' => ConsoleColor.Green,
            'c' => ConsoleColor.Cyan,
            'm' => ConsoleColor.Magenta
        };

        public bool IsValid => GameOptions.ValidChars.Any(c => c.Equals(Key));

        public string ToDisplay(ConsoleColor? foreground = null, double centerPos = .65)
        {
            var percentageSpace = centerPos / (GameOptions.CodeLength * 1.5);
            return new StyledString(Key.ToString(), foreground ?? Foreground, Background, percentageSpace).ToJson() +
                   new StyledString(" ", center: percentageSpace / 2).ToJson();
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
