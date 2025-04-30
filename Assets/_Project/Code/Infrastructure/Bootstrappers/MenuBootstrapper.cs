using _Project.Code.Services.Counter;
using _Project.Code.Services.SceneArgs;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Bootrstrappers
{
    public class MenuBootstrapper : MonoInstaller
    {
        [Inject] private ISceneOutputArgs _args;

        private void Awake()
        {
            InitializeOutputArgs();
        }

        public override void InstallBindings()
        {
            Container.Bind<Counter>().To<Counter>().AsSingle();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Container.Resolve<Counter>().Increment();
        }

        private void InitializeOutputArgs() =>
            _args.Output.Bind<string>().FromInstance("Hi from Menu!");
    }
}