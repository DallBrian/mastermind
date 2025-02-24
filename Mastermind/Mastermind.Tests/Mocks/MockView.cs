using System.Diagnostics;
using Mastermind.Display;
using Mastermind.State;

namespace Mastermind.Tests.Mocks
{
    public class MockView : View
    {
        private const int Interval = 50;

        public string? Text { get; private set; }

        public override void PrintScreen(ApplicationState state)
        {
            Text = GetStringView(state);
            _printScreenCalled = true;
        }

        private bool _printScreenCalled;

        public void WaitForPrintScreen()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while (!_printScreenCalled && stopWatch.ElapsedMilliseconds < 500) Thread.Sleep(Interval);
            _printScreenCalled = false;
        }

        public string? Input { get; private set; }
        public ConsoleKeyInfo? InputKey { get; private set; }

        public override string GetResponse()
        {
            while (Input is null) Thread.Sleep(Interval);

            var input = Input;
            Input = null;
            return input;
        }

        public override ConsoleKeyInfo GetKeyResponse()
        {
            while (InputKey is null) Thread.Sleep(Interval);

            var key = InputKey.Value;
            InputKey = null;
            return key;
        }

        public void SendResponse(string response)
        {
            Input = response;
            WaitForPrintScreen();
        }

        public void SendResponse(ConsoleKey key)
        {
            // Currently we're using the enum in the app code so the actual unicode char doesn't really matter
            SendResponse('#', key);
        }

        public void SendResponse(char keyChar, ConsoleKey key, bool shift = false, bool alt = false, bool control = false)
        {
            InputKey = new ConsoleKeyInfo(keyChar, key, shift, alt, control);
            WaitForPrintScreen();
        }
    }
}
