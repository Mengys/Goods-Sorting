using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.StateMachine;
using Zenject;

namespace _Project.Code.UI.Buttons
{
    public class PlayButton : ButtonClickHandler
    {
        [Inject] private IStateMachine<GameStateId> _stateMachine;
        
        protected override void OnClicked() => _stateMachine.Enter(GameStateId.Gameplay);    
    }
}