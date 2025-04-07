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

        public override string ToString()
        {
            return string.Join(',', ColorCode);
        }
    }

    public class ColorKey(char key)
    {
        private readonly char[] _validChars = ['r', 'b', 'y', 'g'];

        public char Key { get; set; } = key;

        public bool IsValid => _validChars.Any(c => c.Equals(Key));

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
