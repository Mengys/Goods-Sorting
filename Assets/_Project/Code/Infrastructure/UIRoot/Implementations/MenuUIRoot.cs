using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.UIRoot.Implementations
{
    public class MenuUIRoot : MonoInstaller, IUIRoot
    {
        [Inject] private ISceneUIRootSetter _sceneUIRootSetter;
        
        public Transform Transform => transform;
        public DiContainer Container => base.Container;
        
        public override void InstallBindings() => 
            _sceneUIRootSetter.Set(this);
        
        private void OnDestroy() =>
            _sceneUIRootSetter?.Cleanup();
    }
}