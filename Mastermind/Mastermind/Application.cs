using Mastermind.Display;
using System.ComponentModel;
using Mastermind.State;

namespace Mastermind
{
    public class Application
    {
        private BackgroundWorker? _worker;
        private readonly IView _view;
        private readonly ApplicationState _state;

        public Application(IView viewController, ApplicationState? state = null)
        {
            _view = viewController;
            _state = state ?? new ApplicationState();
        }

        public bool IsRunning => _worker?.IsBusy ?? false;

        public void Run()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += ApplicationLoop;
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// Application lifecycle based on user responses and current phase of application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationLoop(object? sender, DoWorkEventArgs e)
        {
            do
            {
                _view.PrintScreen(_state);
                switch (_state.Phase)
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

            } while (_state.Phase != AppPhase.Exited);
        }

        private void HandleMainMenuResponse(ConsoleKeyInfo response)
        {
            switch (response.Key)
            {
                case ConsoleKey.D1:
                    _state.CurrentGame = new GameState();
                    _state.Phase = AppPhase.InGame;
                    break;
                case ConsoleKey.Escape:
                    _state.Phase = AppPhase.Exited;
                    break;
            }
        }

        private void HandleInGameResponse(string response)
        {
            throw new NotImplementedException();
        }
    }
}
