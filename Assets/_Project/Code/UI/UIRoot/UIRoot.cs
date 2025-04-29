using System.Collections.Generic;
using System.Linq;
using _Project.Code.UI.CounterView;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.UIRoot
{
    public class UIRoot : MonoInstaller, IUIRoot
    {
        public override void InstallBindings() => 
            Container.BindInterfacesAndSelfTo<UIRoot>().FromInstance(this).AsSingle();

        public Transform Transform => transform;
        public DiContainer Container => base.Container;
        
        public List<TypedCounterView> Counters => GetComponentsInChildren<TypedCounterView>().ToList();
    }
}