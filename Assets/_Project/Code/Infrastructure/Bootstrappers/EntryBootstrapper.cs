using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.State;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class EntryBootstrapper : MonoInstaller 
    {
        [Inject] private IStateMachine<GameStateId> _stateMachine;
        
        private void Awake() => _stateMachine.Enter(GameStateId.Menu);
        
        public override void InstallBindings()
        {
        }
    }
}