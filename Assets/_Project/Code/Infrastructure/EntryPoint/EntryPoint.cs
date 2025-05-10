using System;
using System.Threading.Tasks;
using _Project.Code.Gameplay.Wallet.Infrastructure.Bootstrappers;
using _Project.Code.Infrastructure.Configs;
using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.Factory;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Services.AssetsLoading;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.Factories.UI;
using _Project.Code.Services.ParticlesPlayer;
using _Project.Code.Services.SceneArgs;
using _Project.Code.Services.SceneLoading;
using _Project.Code.Services.SoundPlayer;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.EntryPoint
{
    public class EntryPoint : MonoInstaller
    {
        [SerializeField] private ProjectUIRoot _uiRoot;

        private async void Awake()
        {
            await LoadPlayerProgressAsync();
            
            Container
                .Resolve<IStateMachine<GameStateId>>()
                .Enter(GameStateId.Entry);
        }

        private async Task LoadPlayerProgressAsync()
        {
            var playerProgress = Container.Resolve<PlayerProgress>();
            var dataService = Container.Resolve<IDataPersistenceService<PlayerProgress>>();

            var data = await dataService.LoadAsync();
           
            if (data != null)
            {
                playerProgress.Coins = data.Coins;
                playerProgress.Level = data.Level;
            }
        }

        private async void OnApplicationQuit()
        {
            var dataService = Container.Resolve<IDataPersistenceService<PlayerProgress>>();
            var playerProgress = Container.Resolve<PlayerProgress>();

            await dataService.SaveAsync(playerProgress);
        }

        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindCoroutinePerformer();
            BindConfigProvider();
            
            BindProjectUIRoot();
            BindLoadingCurtain();
            BindDataPersistenceService();

            Container.BindInterfacesAndSelfTo<PlayerProgress>().AsSingle();

            Container.BindInterfacesAndSelfTo<AssetLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneArgs>().AsSingle();

            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<SoundPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<ParticlesPlayer>().AsSingle();
        }

        private void BindDataPersistenceService()
        {
            var dataService =
                new JsonDataPersistenceService<PlayerProgress>("player_progress.json");

            Container.BindInterfacesAndSelfTo<IDataPersistenceService<PlayerProgress>>()
                .FromInstance(dataService)
                .AsSingle();
        }

        private void BindProjectUIRoot() => 
            Container.BindInterfacesAndSelfTo<ProjectUIRoot>().FromInstance(_uiRoot).AsSingle();

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

        private void BindLoadingCurtain() => 
            Container.Bind<LoadingCurtain>().FromInstance(_uiRoot.LoadingCurtain).AsSingle();

        private void BindConfigProvider()
        {
            Container.BindInterfacesAndSelfTo<ConfigProvider>()
                .FromMethod(ctx =>
                {
                    var loader = ctx.Container.Resolve<IAssetsLoader>();
                    
                    var gameConfig = loader.Load<GameConfig>(ResourcesPaths.GameConfig);

                    if (gameConfig is null)
                        throw new NullReferenceException("Game config is null");

                    return new ConfigProvider(gameConfig);
                })
                .AsSingle();
        }
    }
}
