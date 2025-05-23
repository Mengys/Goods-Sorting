using _Project.Code.Data.Services;
using _Project.Code.Gameplay.Counter;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Services.ProgressProvider;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.UI.Elements
{
    public class GameplayUpperBar : MonoBehaviour
    {
        [field: SerializeField] private TMP_Text _timerText;
        [field: SerializeField] private TMP_Text _levelText;
        [field: SerializeField] private TMP_Text _scoreText;

        private ICounter<Score> _scoreCounter;
        private ITimer _timer;
        private IProgressProvider _progressProvider;

        private readonly CompositeDisposable _disposable = new();

        [Inject]
        public void Construct(
            ITimer timer,
            IProgressProvider progressProvider,
            ICounter<Score> scoreCounter)
        {
            _progressProvider = progressProvider;
            _timer = timer;
            _scoreCounter = scoreCounter;
        }

        private void Initialize()
        {
            _levelText.text = DataTextFormatter.LevelShort(_progressProvider.PlayerProgress.Level.Number);

            _scoreCounter.Reactive
                .Subscribe(v => _scoreText.text = DataTextFormatter.Score(v))
                .AddTo(_disposable);

            _timer.RemainingSeconds
                .Subscribe(v => _timerText.text = DataTextFormatter.Timer(v))
                .AddTo(_disposable);
        }

        private void Start() => Initialize();

        private void OnDestroy() => _disposable.Dispose();
    }
}