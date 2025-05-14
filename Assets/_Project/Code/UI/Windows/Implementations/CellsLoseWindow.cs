using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.StateMachine;
using _Project.Code.UI.Windows.Base;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.UI.Windows.Implementations
{
    public class CellsLoseWindow : Window
    {
        [SerializeField] private Button _retry;

        private readonly CompositeDisposable _disposable = new();
        private IStateMachine<GameStateId> _stateMachine;

        [Inject]
        public void Construct(IStateMachine<GameStateId> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Initialize()
        {
            _retry.OnClickAsObservable()
                .Subscribe(_ => _stateMachine.Enter(GameStateId.Menu))
                .AddTo(_disposable);
        }

        public override void OnDestroy() =>
            _disposable.Dispose();
    }
}