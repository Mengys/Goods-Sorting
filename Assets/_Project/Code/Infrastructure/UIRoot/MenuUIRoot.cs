using System.Collections.Generic;
using System.Linq;
using _Project.Code.Services.UIFactory.WindowOpener;
using _Project.Code.UI.CounterView;
using _Project.Code.UI.Window;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.UIRoot
{
    public class MenuUIRoot : MonoInstaller, IUIRoot
    {
        [Inject] private ISceneUIRootSetter _sceneUIRootSetter;
        
        public Transform Transform => transform;
        public DiContainer Container => base.Container;
        
        private List<TypedCounterView> Counters => 
            GetComponentsInChildren<TypedCounterView>().ToList();   
        
        public override void InstallBindings() => 
            _sceneUIRootSetter.Set(this);
        
        private void OnDestroy() =>
            _sceneUIRootSetter.Cleanup();
    }
}