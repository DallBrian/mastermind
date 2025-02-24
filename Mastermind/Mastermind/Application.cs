using Mastermind.Display;
using Mastermind.Models;
using System.ComponentModel;

namespace Mastermind
{
    public class Application
    {
        private BackgroundWorker _worker;
        private readonly IView _view;
        public readonly ApplicationState State;

        public Application(IView viewController)
        {
            _view = viewController;
            State = new ApplicationState();
        }

        public bool IsRunning => _worker.IsBusy;
        
        public void Run()
        {
            _worker = new();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += ApplicationLoop;
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// Application lifecycle based on user responses and current phase of application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationLoop(object sender, DoWorkEventArgs e)
        {
            do
            {
                _view.PrintScreen(State);
                switch (State.Phase)
                {
                    case AppPhase.MainMenu:
                    {
                        var playerInput = _view.GetKeyResponse();
                        HandleMainMenuResponse(playerInput);
                        break;
                    }
                    case AppPhase.InGame:
                    {
                        var playerInput = _view.GetResponse();
                        HandleInGameResponse(playerInput);
                        break;
                    }
                }

            } while (State.Phase != AppPhase.Exited);
        }

        private void HandleMainMenuResponse(ConsoleKeyInfo response)
        {
            switch (response.Key)
            {
                case ConsoleKey.D1:
                    State.CurrentGame = new GameState();
                    State.Phase = AppPhase.InGame;
                    break;
                case ConsoleKey.Escape:
                    State.Phase = AppPhase.Exited;
                    break;
            }
        }

        private void HandleInGameResponse(string response)
        {
            
        }
    }
}
