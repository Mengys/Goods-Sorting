using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.SceneArgs;
using Zenject;

namespace _Project.Code.Infrastructure.Entry.Bootstrappers
{
    public class MenuBootstrapper : MonoInstaller
    {
        [Inject] private IStateMachine<GameStateId> _stateMachine;
        [Inject] private ISceneOutputArgs _args;

        private void Awake()
        {
            _args.Output.Bind<string>().FromInstance("Hi from Menu!"); 
            _stateMachine.Enter(GameStateId.Gameplay);            
        }
        
        public override void InstallBindings()
        {
        }
    }
}