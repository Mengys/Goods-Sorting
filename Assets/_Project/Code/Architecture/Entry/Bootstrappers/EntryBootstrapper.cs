using _Project.Code.Architecture.Services.GameStateMachine;
using Zenject;

namespace _Project.Code.Architecture.Entry.Bootstrappers
{
    public class EntryBootstrapper : MonoInstaller 
    {
        [Inject] private IStateMachine<GameState> _stateMachine;
        
        private void Awake() => _stateMachine.Enter(GameState.Menu);
        
        public override void InstallBindings()
        {
        }
    }
}