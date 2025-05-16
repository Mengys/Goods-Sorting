using _Project.Code.Gameplay.Counter;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.Gameplay.LevelFlow;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Gameplay.WinIncome;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.Factories.Level;
using _Project.Code.Services.PauseHandler;
using _Project.Code.Services.SceneArgs;
using _Project.Code.UI.Buttons.Booster;
using _Project.Code.UI.Buttons.Window;
using _Project.Code.UI.Windows.Implementations;
using GoogleMobileAds.Api;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;
        private BannerView _bannerView;

#if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif
        
        private void Start()
        {
            Container.Resolve<ILevelFlow>().Initialize();
            Container.Resolve<LoadingCurtain>().Hide();
            
            CreateBannerView();   
            _bannerView.LoadAd(new AdRequest());
        }
        
        public void CreateBannerView()
        {
            Debug.Log("Creating banner view");

            // Create a 320x50 banner at top of the screen
            var adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
            _bannerView = new BannerView(_adUnitId, adSize, AdPosition.Bottom);
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
           //     .OnInstantiated<ComboHandler>((ctx, instance) => 
              //      instance.Initialize(ctx.Container.Resolve<ItemCollectHandler>()));
            
            Container.BindInterfacesAndSelfTo<ItemCollectHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreIncomeHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<Counter<Score>>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BoosterInventory>().AsSingle();
            Container.BindInterfacesAndSelfTo<Timer>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoosterUser>().AsSingle();
        }
    }
}