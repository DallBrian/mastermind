using Mastermind.Display;

namespace Mastermind.State
{
    public class CodeEntry
    {
        private readonly int _maxLength = 4;

        public CodeEntry(string codeString)
        {
            ColorCode = codeString.ToCharArray().Select(c => new ColorKey(c)).ToList();
        }

        public List<ColorKey> ColorCode { get; }

        public bool IsValid => ColorCode.Count == _maxLength && ColorCode.All(c => c.IsValid);

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
        private readonly char[] _validChars = ['r', 'b', 'y', 'g'];

        public char Key { get; set; } = key;

        private ConsoleColor Foreground = key switch
        {
            'r' => ConsoleColor.Red,
            'b' => ConsoleColor.Blue,
            'y' => ConsoleColor.Yellow,
            'g' => ConsoleColor.Green
        };

        private ConsoleColor Background = key switch
        {
            'r' => ConsoleColor.Red,
            'b' => ConsoleColor.Blue,
            'y' => ConsoleColor.Yellow,
            'g' => ConsoleColor.Green
        };

        public bool IsValid => _validChars.Any(c => c.Equals(Key));

        public string ToDisplay()
        {
            return new StyledString(Key.ToString(), Foreground, Background, .65/8).ToJson() +
                   new StyledString(" ", center: .65 / 8).ToJson();
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
