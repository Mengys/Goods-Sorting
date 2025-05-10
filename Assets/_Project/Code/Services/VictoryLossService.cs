using _Project.Code.Gameplay.Timer;

namespace _Project.Code.Services
{
    public class VictoryLossService
    {
        private Timer _timer;
        private WindowService _windowService;

        public VictoryLossService(Timer timer, WindowService windowService)
        {
            _timer = timer;
            _windowService = windowService;
        }

        public void Update()
        {
            if (_timer.SecondsLeft <= 0)
            {
                _windowService.ShowLossWindow();
            }
        }
    }
}
