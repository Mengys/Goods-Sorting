using _Project.Code.Gameplay.Score;
using _Project.Code.Gameplay.Wallet.Infrastructure.Bootstrappers;
using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.State;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.UI.Windows.Implementations.LevelInfo
{
    public class WinWindow : Window
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _continueIncome;
        [SerializeField] private TMP_Text _multiplyIncome;
        
        [SerializeField] private Button _continue;
        [SerializeField] private Button _multiply;
        
        private readonly CompositeDisposable _disposer = new();

        [Inject]
        private void Construct(Score score, IStateMachine<GameStateId> stateMachine, PlayerProgress playerProgress)
        {
            _score.text = score.Value.ToString();
            
            _continueIncome.text = ToCoins(score.Value).ToString();
            _multiplyIncome.text = ToMultiplyCoins(score.Value).ToString();

            _continue.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    playerProgress.Coins += ToCoins(score.Value);
                    stateMachine.Enter(GameStateId.Menu);
                })
                .AddTo(_disposer);
            
            _multiply.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    playerProgress.Coins += ToMultiplyCoins(score.Value);
                    stateMachine.Enter(GameStateId.Menu);
                })
                .AddTo(_disposer);
        }
        
        private int ToCoins(int score) => score;
        
        private int ToMultiplyCoins(int score) => score * 5;
        
        public override void OnDestroy() =>
            _disposer.Dispose();
    }
}