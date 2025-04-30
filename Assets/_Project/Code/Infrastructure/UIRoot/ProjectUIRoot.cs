using _Project.Code.Services.Curtain;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.UIRoot
{
    public class ProjectUIRoot : MonoInstaller, IUIRoot, ISceneUIRootSetter
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private LoadingCurtain _loadingCurtain;

        private IUIRoot _sceneUIRoot;

        public Transform Transform => _sceneUIRoot?.Transform ?? _canvas.transform;
        public DiContainer Container => _sceneUIRoot?.Container ?? base.Container;
        public LoadingCurtain LoadingCurtain => _loadingCurtain;

        public void Set(IUIRoot sceneUIRoot) => _sceneUIRoot = sceneUIRoot;
        
        public void Cleanup() => _sceneUIRoot = null;
    }
}