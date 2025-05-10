using _Project.Code.Gameplay.Levels;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.SceneArgs;
using _Project.Code.UI.Windows.Implementations.LevelInfo;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class MenuBootstrapper : MonoInstaller
    {
        [Inject] private ISceneOutputArgs _args;

        private void Awake() =>
            InitializeOutputArgs();

        private void Start() =>
            Container.Resolve<LoadingCurtain>().Hide();

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelInfo>()
                .FromInstance(new LevelInfo { Number = 1, Difficulty = DifficultyType.Easy }).AsSingle();

            Container.BindInterfacesAndSelfTo<LevelInfoBoosterProvider>().AsSingle();
        }

        private void InitializeOutputArgs() =>
            _args.Output.Bind<LevelStartData>()
                .FromInstance(new LevelStartData { InitialBoosterId = null });
    }
}