using System;
using Zenject;

namespace _Project.Code
{
    public class MenuBootstrapper : MonoInstaller
    {
        [Inject] private IStateMachine<GameState> _stateMachine;

        private void Awake()
        {
            _stateMachine.Enter(GameState.Gameplay);            
        }
    }
}