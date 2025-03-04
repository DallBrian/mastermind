namespace Mastermind.State
{
    public class GameOptions
    {
        public static int CodeLength { get; set; } = 4;

        public static List<char> ValidChars => "rybgcm".ToCharArray(0, CodeLength).ToList();
    }
}
