using _Project.Code.Services.SceneArgs;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class MenuBootstrapper : MonoInstaller
    {
        [Inject] private ISceneOutputArgs _args;
        
        private void Awake() => 
            InitializeOutputArgs();

        public override void InstallBindings()
        {
        }

        private void InitializeOutputArgs() =>
            _args.Output.Bind<LevelStartData>()
                .FromInstance(new LevelStartData { InitialBoosterId = null });
    }
}