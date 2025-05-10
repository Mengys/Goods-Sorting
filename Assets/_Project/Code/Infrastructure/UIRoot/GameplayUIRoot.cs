using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.UIRoot
{
    public class GameplayUIRoot : MonoInstaller, IUIRoot
    {
        [field: SerializeField] public UpperBar UpperBar { get; private set; }
        
        [Inject] private ISceneUIRootSetter _sceneUIRootSetter;
        
        public Transform Transform => transform;
        public DiContainer Container => base.Container;
        
        public override void InstallBindings()
        {
            Container.Bind<GameplayUIRoot>().FromInstance(this).AsSingle();
            _sceneUIRootSetter.Set(this);
        }
    }
}