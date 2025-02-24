using Mastermind.Display;

namespace Mastermind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var view = new View();
            var app = new Application(view);
            app.Run();
            while (app.IsRunning) { }
        }
    }
}
