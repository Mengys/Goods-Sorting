using _Project.Code.Gameplay;
using _Project.Code.Gameplay.Combo;
using _Project.Code.Gameplay.Counter;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.Gameplay.IncomeHandling.ScoreIncome;
using _Project.Code.Gameplay.IncomeHandling.WinIncome;
using _Project.Code.Gameplay.LevelFlow;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Services.AdShower;
using _Project.Code.Services.BoosterUser;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.Factories.Level;
using _Project.Code.Services.Factories.UI;
using _Project.Code.Services.PauseHandler;
using _Project.Code.Services.SceneArgs;
using _Project.Code.UI.Buttons.Window;
using _Project.Code.UI.Windows.Implementations;
using GoogleMobileAds.Api;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;
        private BannerView _bannerView;

        private void Awake()
        {
            var initialBoosterId = _inputArgs.Input.Resolve<LevelInitialBooster>().Value;
            Container.Resolve<ILevelFlow>().Initialize(initialBoosterId);
            Container.Resolve<IAdShower>().ShowBanner();
        }
        
        private void Start()
        {
            Container.Resolve<LoadingCurtain>().Hide();
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelFactory>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ItemGrid>()
                .FromMethod(ctx => ctx.Container.Resolve<IFactory<ItemGrid>>().Create())
                .AsSingle();

            Container.BindInterfacesAndSelfTo<LevelFlow>()
                .FromMethod(ctx => ctx.Container.Resolve<IFactory<LevelFlow>>().Create())
                .AsSingle();

            Container.BindInterfacesAndSelfTo<GameOverHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<WinIncomeHandler>().AsSingle();

            Container.BindInterfacesAndSelfTo<ComboHandler>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CollectedItemsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreIncomeHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<Counter<Score>>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Timer>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoosterUser>().AsSingle();
        }
    }
}