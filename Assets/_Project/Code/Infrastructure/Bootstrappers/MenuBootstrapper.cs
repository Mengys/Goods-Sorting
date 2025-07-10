using _Project.Code.Data.Static.Level;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.SceneArgs;
using _Project.Code.UI.Windows.Implementations;
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
        }

        private void InitializeOutputArgs() =>
            _args.Output.Bind<LevelInitialBooster>().AsSingle();
    }
}