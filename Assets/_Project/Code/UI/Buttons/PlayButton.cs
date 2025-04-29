using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.State;
using Zenject;

namespace _Project.Code.UI.Buttons
{
    public class PlayButton : ButtonClickHandler
    {
        [Inject] private IStateMachine<GameStateId> _stateMachine;
        
        protected override void OnClicked() => _stateMachine.Enter(GameStateId.Gameplay);    
    }
}