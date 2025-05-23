using System;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.PauseHandler;
using _Project.Code.UI.Buttons.Window;
using R3;

namespace _Project.Code.Gameplay.Combo
{
    public class  ComboHandler : IComboHandler, IDisposable, IPausable
    {
        private readonly ReactiveProperty<int> _level = new();
        private readonly ReactiveProperty<float> _levelProgress = new();

        private readonly CompositeDisposable _disposable = new();

        private readonly ITimer _timer;

        private readonly float _firstLevelDuration;

        private float _levelDuration;

        public ComboHandler(ICoroutinePerformer coroutinePerformer)
        {
            _timer = new Timer.Timer(coroutinePerformer);
            _levelDuration = _firstLevelDuration = 20f;
        }

        public ReadOnlyReactiveProperty<int> Level => _level;

        public ReadOnlyReactiveProperty<float> LevelProgress =>
            _timer.RemainingSeconds
                .Select(v => v / _levelDuration)
                .ToReadOnlyReactiveProperty();
        
        public void Initialize(IItemCollectHandler itemCollectHandler)
        {
            itemCollectHandler.ItemsHandled
                .Subscribe(_ => AdjustLevel())
                .AddTo(_disposable);

            _timer.Elapsed
                .Subscribe(_ => _level.Value = 0)
                .AddTo(_disposable);

            _timer.RemainingSeconds
                .Subscribe(v => _levelProgress.Value = v / _levelDuration)
                .AddTo(_disposable);
        }

        private void AdjustLevel()
        {
            _level.Value++;

            if (_timer.IsRunning == false)
            {
                _levelDuration = _firstLevelDuration;
                _timer.Setup(_levelDuration);
                _timer.Start();
            }
            else
            {
                _levelDuration *= 0.7f;

                _timer.Stop();
                _timer.Setup(_levelDuration);
                _timer.Start();
            }
        }

        public void Dispose()
        {
            _level?.Dispose();
            _disposable?.Dispose();
        }

        public void Pause() => _timer.Stop();
        public void Resume() => _timer.Start();
    }
}