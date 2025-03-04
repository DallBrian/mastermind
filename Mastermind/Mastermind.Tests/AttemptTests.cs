using Mastermind.State;

namespace Mastermind.Tests
{
    public class AttemptTests : BaseTest
    {
        public static TestCaseData[] AttemptTestCases =
        [
            new(new CodeEntry("rbyg"), new CodeEntry("rbyg"), 4, 0),
            new(new CodeEntry("rrbr"), new CodeEntry("rbyr"), 2, 1),
            new(new CodeEntry("rrrr"), new CodeEntry("rbyg"), 1, 0),
            new(new CodeEntry("bbbb"), new CodeEntry("gygb"), 1, 0)
        ];

        [TestCaseSource(nameof(AttemptTestCases))]
        public void AttemptCalculations(CodeEntry guess, CodeEntry gameCode, int expectedFullMatches, int expectedColorMatches)
        {
            var attempt = new Attempt(guess, gameCode);
            Assert.That(attempt.FullMatch, Is.EqualTo(expectedFullMatches));
            Assert.That(attempt.ColorMatch, Is.EqualTo(expectedColorMatches));
        }
    }
}
