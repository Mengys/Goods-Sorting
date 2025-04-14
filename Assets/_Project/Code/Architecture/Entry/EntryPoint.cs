using _Project.Code.Architecture;
using Zenject;

namespace _Project.Code
{
    public class EntryPoint : MonoInstaller
    {
        private void Awake()
        {
            var stateMachine = Container.Resolve<IStateMachine<GameState>>();
            stateMachine.Enter(GameState.Entry);
        }

        public override void InstallBindings()
        {
            Container.Bind<ResourcesLoader>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

            Container.Bind<ICoroutinePerformer>()
                .FromInstance(new CoroutinePerformer(this))
                .AsSingle();


            Container.Bind<GameConfig>()
                .FromScriptableObjectResource(ResourcesPaths.GameConfigPath)
                .AsSingle();


            Container.Bind<LoadingCurtain>()
                .FromComponentInNewPrefabResource(ResourcesPaths.LoadingCurtainPath)
                .AsSingle();
        }
    }
}