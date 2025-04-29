using _Project.Code.Services.Counter;
using _Project.Code.Services.SceneArgs;
using _Project.Code.Services.UIFactory;
using _Project.Code.UI.UIRoot;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Bootrstrappers
{
    public class MenuBootstrapper : MonoInstaller
    {
        [Inject] private ISceneOutputArgs _args;
        [Inject] private IUIRootUser _uiRootUser;

        private void Awake()
        {
            InitializeRoot();
            InitializeUI();
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

        private void OnDestroy() =>
            _uiRootUser.Cleanup();

        private void InitializeOutputArgs() =>
            _args.Output.Bind<string>().FromInstance("Hi from Menu!");

        private void InitializeRoot() =>
            _uiRootUser.Initialize(Container.Resolve<IUIRoot>());

        private void InitializeUI()
        {
            var uiRoot = Container.Resolve<UIRoot>();
            var counter = Container.Resolve<Counter>();

            uiRoot.Counters.ForEach(view => counter.Value.Subscribe(view.SetCounter));
        }
    }
}