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
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;

        private void Start()
        {
            Container.Resolve<ILevelFlow>().Initialize();
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