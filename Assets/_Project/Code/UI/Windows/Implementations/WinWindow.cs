using _Project.Code.Data.Services;
using _Project.Code.Data.Static.GameState;
using _Project.Code.Gameplay.Counter;
using _Project.Code.Gameplay.WinIncome;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.Services.StateMachine;
using _Project.Code.UI.Windows.Base;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.UI.Windows.Implementations
{
    public class WinWindow : Window
    {
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _continueIncome;
        [SerializeField] private TMP_Text _multiplyIncome;

        [SerializeField] private Button _continue;
        [SerializeField] private Button _multiply;

        private ICounter<Score> _scoreCounter;
        private IWinIncomeHandler _winIncomeHandler;
        private IStateMachine<GameStateId> _stateMachine;
        
        private readonly CompositeDisposable _disposer = new();
        private IProgressProvider _progressProvider;

        [Inject]
        private void Construct(
            ICounter<Score> scoreCounter,
            IWinIncomeHandler winIncomeHandler,
            IStateMachine<GameStateId> stateMachine,
            IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
            _stateMachine = stateMachine;
            _winIncomeHandler = winIncomeHandler;
            _scoreCounter = scoreCounter;
        }

        public override void Initialize()
        {
            int level = _progressProvider.PlayerProgress.Level.Number;
            
            _level.text = DataTextFormatter.Level(level);
            _continueIncome.text = _winIncomeHandler.DefaultIncome.ToString();
            _multiplyIncome.text = _winIncomeHandler.RewardedIncome.ToString();
            _score.text = _scoreCounter.Value.ToString();

            _continue.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _winIncomeHandler.HandleDefault();
                    _stateMachine.Enter(GameStateId.Menu);
                })
                .AddTo(_disposer);

            _multiply.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _winIncomeHandler.HandleRewarded();
                    _stateMachine.Enter(GameStateId.Menu);
                })
                .AddTo(_disposer);
        }

        public override void OnDestroy() =>
            _disposer.Dispose();
    }
}