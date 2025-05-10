using _Project.Code.Gameplay.Score;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Gameplay.Wallet.Infrastructure.Bootstrappers;
using _Project.Code.UI.Windows.Implementations.LevelInfo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.Infrastructure.UIRoot
{
    public class UpperBar : MonoBehaviour
    {
        private Score _score;
        private Timer _timer;
        [field: SerializeField] public TimerView TimerView { get; private set; }
        [field: SerializeField] public ScoreView ScoreView { get; private set; }
        [field: SerializeField] public TMP_Text LevelText { get; private set; }
        [field: SerializeField] public Button PauseButton { get; private set; }

        [Inject]
        public void Construct(PlayerProgress playerProgress, Score score, Timer timer)
        {
            LevelText.text = $"Lv.{playerProgress.Level.Number}";
            
            _score = score;
            _score.Changed += ScoreView.UpdateView;

            _timer = timer;
            _timer.Updated += TimerView.UpdateView;
        }
        
        private void Start()
        {
            ScoreView.UpdateView(_score.Value);
            TimerView.UpdateView(_timer.SecondsLeft);
        }

        private void OnDestroy()
        {
            _timer.Updated -= TimerView.UpdateView;
            _score.Changed -= ScoreView.UpdateView;
        }
    }
}