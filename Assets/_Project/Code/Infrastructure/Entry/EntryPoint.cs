using _Project.Code.Infrastructure.Services.ConfigProvider;
using _Project.Code.Infrastructure.Services.CoroutinePerformer;
using _Project.Code.Infrastructure.Services.Curtain;
using _Project.Code.Infrastructure.Services.GameStateMachine;
using _Project.Code.Infrastructure.Services.GameStateMachine.State;
using _Project.Code.Infrastructure.Services.ResourcesLoading;
using _Project.Code.Infrastructure.Services.SceneArgs;
using _Project.Code.Infrastructure.Services.SceneLoading;
using Zenject;

namespace _Project.Code.Infrastructure.Entry
{
    public class EntryPoint : MonoInstaller
    {
        private void Awake()
        {
            var stateMachine = Container.Resolve<IStateMachine<GameStateId>>();
            stateMachine.Enter(GameStateId.Entry);
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