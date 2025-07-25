using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.StateMachine;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Code.UI.Buttons.Window
{
    public interface IItemCollectHandler
    {
        Observable<int> ItemsHandled { get; }
        void Handle(int itemsCount = 1);
    }

    public interface IScoreIncomeHandler
    {
        void Handle(int itemsCount);
    }

    public interface IComboHandler
    {
        public ReadOnlyReactiveProperty<float> LevelProgress { get; }
        public ReadOnlyReactiveProperty<int> Level { get; }
        void Initialize(IItemCollectHandler itemCollectHandler);
    }


    public class MenuButton : ButtonClickHandler
    {
        private IStateMachine<GameStateId> _stateMachine;

        [Inject]
        public void Construct(IStateMachine<GameStateId> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        protected override void OnClicked() =>
            //_stateMachine.Enter(GameStateId.Menu);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}