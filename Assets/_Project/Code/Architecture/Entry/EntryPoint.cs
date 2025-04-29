using _Project.Code.Architecture.Services.ConfigProvider;
using _Project.Code.Architecture.Services.CoroutinePerformer;
using _Project.Code.Architecture.Services.Curtain;
using _Project.Code.Architecture.Services.GameStateMachine;
using _Project.Code.Architecture.Services.ResourcesLoading;
using _Project.Code.Architecture.Services.SceneArgs;
using _Project.Code.Architecture.Services.SceneLoading;
using Zenject;

namespace _Project.Code.Architecture.Entry
{
    public class EntryPoint : MonoInstaller
    {
        private void Awake()
        {
            var stateMachine = Container.Resolve<IStateMachine<GameState>>();
            stateMachine.Enter(GameState.Entry);
        }

        public override void InstallBindings()
        {
            Container.Bind<ResourcesLoader>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneArgs>().AsSingle();

            Container.Bind<ICoroutinePerformer>()
                .FromInstance(new CoroutinePerformer(this))
                .AsSingle();

            Container.Bind<ConfigProvider>()
                .FromScriptableObjectResource(ResourcesPaths.ConfigProviderPath)
                .AsSingle();
            
            Container.Bind<LoadingCurtain>()
                .FromComponentInNewPrefabResource(ResourcesPaths.LoadingCurtainPath)
                .AsSingle();
        }
    }
}