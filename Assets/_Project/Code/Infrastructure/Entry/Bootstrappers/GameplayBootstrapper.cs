using _Project.Code.Infrastructure.Services.SceneArgs;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Entry.Bootstrappers
{
    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;

        private void Awake()
        {
            var message = _inputArgs.Input.Resolve<string>();
            Debug.Log(message);
        }

        public override void InstallBindings()
        {
        }
    }
}