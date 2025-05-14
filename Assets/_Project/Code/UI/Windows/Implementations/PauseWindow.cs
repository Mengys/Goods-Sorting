using _Project.Code.Services.PauseHandler;
using _Project.Code.UI.Windows.Base;
using Zenject;

namespace _Project.Code.UI.Windows.Implementations
{
    public class PauseWindow : Window
    {
        private IPauseHandler _pauseHandler;

        [Inject]
        public void Construct(IPauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
        }

        public override void Initialize() =>
            _pauseHandler.Pause();

        public override void OnDestroy() =>
            _pauseHandler.Resume();
    }
}