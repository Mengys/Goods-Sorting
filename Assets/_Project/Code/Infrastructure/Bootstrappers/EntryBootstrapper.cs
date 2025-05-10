using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class EntryBootstrapper : MonoInstaller
    {
        [Inject] private IStateMachine<GameStateId> _stateMachine;

        private void Start() => 
            _stateMachine.Enter(GameStateId.Menu);

        public override void InstallBindings()
        {
        }
    }
}