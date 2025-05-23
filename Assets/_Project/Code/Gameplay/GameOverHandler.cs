using _Project.Code.Services.Factories.UI;
using _Project.Code.Services.PauseHandler;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.UI.Windows.Base;

namespace _Project.Code.Gameplay
{
    public class GameOverHandler
    {
        private readonly IPauseHandler _pauseHandler;
        private readonly IWindowFactory _factory;
        private readonly IProgressProvider _progressProvider;

        public GameOverHandler(
            IProgressProvider progressProvider,
            IPauseHandler pauseHandler,
            IWindowFactory factory)
        {
            _progressProvider = progressProvider;
            _factory = factory;
            _pauseHandler = pauseHandler;
        }

        public void HandleTimeLose()
        {
            _pauseHandler.Pause();
            _factory.Create(WindowId.TimeLose);
        }

        public void HandleOutOfFreeCellsLose()
        {
            _pauseHandler.Pause();
            _factory.Create(WindowId.FreeCellsLose);
        }

        public void HandleWin()
        {
            _pauseHandler.Pause();
            _factory.Create(WindowId.Win);
            _progressProvider.IncrementPassedLevel();
        }
    }
}