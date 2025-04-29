using System;
using _Project.Code.Configs;
using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.Factory;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.AssetsLoading;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.ParticlesPlayer;
using _Project.Code.Services.SceneArgs;
using _Project.Code.Services.SceneLoading;
using _Project.Code.Services.SoundPlayer;
using _Project.Code.Services.UIFactory;
using Zenject;

namespace _Project.Code.Infrastructure.Entry
{
    public class EntryPoint : MonoInstaller
    {
        private void Awake()
        {
            Container
                .Resolve<IStateMachine<GameStateId>>()
                .Enter(GameStateId.Entry);
        }

        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindCoroutinePerformer();
            BindLoadingCurtain();
            BindConfigProvider();

            Container.BindInterfacesAndSelfTo<AssetLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneArgs>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<SoundPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<ParticlesPlayer>().AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStatesFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine.GameStateMachine>().AsSingle();
        }

        private void BindCoroutinePerformer()
        {
            Container.Bind<ICoroutinePerformer>()
                .FromInstance(new CoroutinePerformer(this))
                .AsSingle();
        }

        private void BindLoadingCurtain()
        {
            Container.Bind<LoadingCurtain>()
                .FromComponentInNewPrefabResource(ResourcesPaths.LoadingCurtain)
                .AsSingle();
        }

        private void BindConfigProvider()
        {
            Container.BindInterfacesAndSelfTo<ConfigProvider>()
                .FromMethod(ctx =>
                {
                    var loader = ctx.Container.Resolve<IAssetsLoader>();
                    var gameConfig = loader.Load<GameConfig>(ResourcesPaths.GameConfig);

                    if (gameConfig is null)
                        throw new NullReferenceException("Game config is not found");

                    return new ConfigProvider(gameConfig);
                })
                .AsSingle();
        }
    }
}
