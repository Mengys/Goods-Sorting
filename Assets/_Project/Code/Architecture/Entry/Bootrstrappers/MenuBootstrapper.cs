using System;
using Zenject;

namespace _Project.Code
{
    public class MenuBootstrapper : MonoInstaller
    {
        [Inject] private IStateMachine<GameState> _stateMachine;
        [Inject] private ISceneOutputArgs _args;

        private void Awake()
        {
            _args.Output.Bind<string>().FromInstance("Hi from Menu!");
            _stateMachine.Enter(GameState.Gameplay);            
        }
        
        public override void InstallBindings()
        {
        }
    }
}