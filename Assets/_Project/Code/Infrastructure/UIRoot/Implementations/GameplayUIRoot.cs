using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.UI.Elements;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.UIRoot.Implementations
{
    public class GameplayUIRoot : MonoInstaller, IUIRoot
    {
        [field: SerializeField] public GameplayUpperBar GameplayUpperBar { get; private set; }
        
        [Inject] private ISceneUIRootSetter _sceneUIRootSetter;
        [Inject] private IConfigProvider _configProvider;
        //[Inject] private DiContainer _container;

        public Transform Transform => transform;
        public DiContainer Container => base.Container;
        
        public override void InstallBindings()
        {
            Container.Bind<GameplayUIRoot>().FromInstance(this).AsSingle();
            _sceneUIRootSetter.Set(this);
        }

        private void Start() {
            var difficulty = _configProvider.ForLevel(Container.Resolve<IProgressProvider>().PlayerProgress.Level.Id).Value.Difficulty;
            Color color = Color.white;
            switch (difficulty){
                case Data.Static.Level.DifficultyType.Easy:
                    color = new Color(0.6622641f, 0.9870793f, 1f);
                    break;
                case Data.Static.Level.DifficultyType.Medium:
                    color = new Color(0.6622641f, 0.9870793f, 1f);
                    break;
                case Data.Static.Level.DifficultyType.Hard:
                    color = new Color(0.6622641f, 0.9870793f, 1f);
                    break;
            }
            //Camera.main.backgroundColor = color;
        }
    }
}
