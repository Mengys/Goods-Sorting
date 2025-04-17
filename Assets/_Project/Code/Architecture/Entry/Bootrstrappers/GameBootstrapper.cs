using Zenject;

namespace _Project.Code
{
    public class GameBootstrapper : MonoInstaller 
    {
        [Inject] private IStateMachine<GameState> _stateMachine;

        private void Awake() => _stateMachine.Enter(GameState.Menu);
        
        public override void InstallBindings()
        {
        }
    }
}