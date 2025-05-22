using System;
using _Project.Code.Data.Dynamic.PlayerProgress;
using _Project.Code.Data.Static.Game;
using _Project.Code.Data.Static.GameState;
using _Project.Code.Data.Static.Paths;
using _Project.Code.Infrastructure.Bootstrappers;
using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Infrastructure.UIRoot.Implementations;
using _Project.Code.Services.ApplicationLifecycle;
using _Project.Code.Services.AssetsLoading;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.DataPersistence;
using _Project.Code.Services.Factories.GameStates;
using _Project.Code.Services.Factories.UI;
using _Project.Code.Services.ParticlesPlayer;
using _Project.Code.Services.PauseHandler;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.Services.SceneArgs;
using _Project.Code.Services.SceneLoader;
using _Project.Code.Services.SoundPlayer;
using _Project.Code.Services.StateMachine;
using _Project.Code.Services.StateMachine.Game;
using _Project.Code.UI.Elements;
using _Project.Code.UI.Elements.Booster.Factory;
using _Project.Code.Utils;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.EntryPoint
{
    public class EntryPoint : MonoInstaller
    {
        [SerializeField] private ProjectUIRoot _uiRoot;

        private Subject<Unit> _applicationQuit = new();
        private Subject<Unit> _applicationPaused = new();
        private Subject<Unit> _applicationFocused = new();

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BoosterInventoryFactory>().AsSingle();
            
            BindCoroutinePerformer();
            BindAssetLoader();
            BindSceneLoader();

            BindSceneArgs();

            BindAppLifeCycleObserver();
            BindPauseService();

            BindUIFactory();
            BindUIRoot();
            BindCurtain();

            BindGameStateMachine();

            BindDataPersistence();
            BindProgressProvider();
            
            BindFXPlayers();
            BindConfigProvider();
            
            Container.BindInterfacesAndSelfTo<AdShower>().AsSingle();
        }

        private void OnApplicationQuit() => _applicationQuit.OnNext(Unit.Default);

        private void OnApplicationPause(bool pauseStatus) => _applicationPaused.OnNext(Unit.Default);

        private void OnApplicationFocus(bool focusStatus) => _applicationFocused.OnNext(Unit.Default);

        private void Awake()
        {
            Container
                .Resolve<IStateMachine<GameStateId>>()
                .Enter(GameStateId.Entry);
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
        }

        private void BindPauseService()
        {
            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle()
                .OnInstantiated<PauseHandler>((ctx, instance) =>
                    instance.HandleAppLifeCycleEvents(ctx.Container.Resolve<AppLifeCycleEvents>())
                        .AddTo(this));
        }

        private void BindSceneArgs()
        {
            Container.BindInterfacesAndSelfTo<SceneArgs>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
        }

        private void BindAssetLoader()
        {
            Container.BindInterfacesAndSelfTo<AssetLoader>().AsSingle();
        }

        private void BindCurtain()
        {
            Container.Bind<LoadingCurtain>()
                .FromInstance(_uiRoot.LoadingCurtain)
                .AsSingle();
        }

        private void BindUIRoot()
        {
            Container.BindInterfacesAndSelfTo<ProjectUIRoot>()
                .FromInstance(_uiRoot)
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStatesFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindDataPersistence()
        {
            Container.BindInterfacesAndSelfTo<IDataPersistence<PlayerProgress>>()
                .FromMethod(ctx =>
                {
                    var instance = new JsonFileDataPersistence<PlayerProgress>(DataPaths.PlayerProgress);
                    
                    var progressProvider = ctx.Container.Resolve<IProgressProvider>();
                    var appLifeCycleEvents = ctx.Container.Resolve<AppLifeCycleEvents>();

                    instance.HandleAppQuit(progressProvider, appLifeCycleEvents).AddTo(this);
                    
                    return instance;
                })
                .AsSingle();
        }

        private void BindProgressProvider()
        {
            Container.BindInterfacesAndSelfTo<ProgressProvider>().AsSingle();
        }

        private void BindFXPlayers()
        {
            Container.BindInterfacesAndSelfTo<SoundPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<ParticlesPlayer>().AsSingle();
        }

        private void BindCoroutinePerformer()
        {
            Container.Bind<ICoroutinePerformer>()
                .FromInstance(new CoroutinePerformer(this))
                .AsSingle();
        }

        private void BindAppLifeCycleObserver()
        {
            Container.Bind<AppLifeCycleEvents>()
                .FromInstance(new AppLifeCycleEvents(
                    _applicationPaused,
                    _applicationFocused,
                    _applicationQuit))
                .AsSingle();
        }

        private void BindConfigProvider()
        {
            Container.BindInterfacesAndSelfTo<ConfigProvider>()
                .FromMethod(ctx =>
                {
                    var loader = ctx.Container.Resolve<IAssetsLoader>();
                    var config = loader.Load<GameConfig>(ResourcesPaths.GameConfig)
                                 ?? throw new NullReferenceException("Game config is null");
                    return new ConfigProvider(config);
                })
                .AsSingle();
        }
    }
}