using System;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.UIFactory.Config;
using _Project.Code.Services.UIFactory.WindowFactory;
using _Project.Code.Services.UIFactory.WindowOpener;
using _Project.Code.UI.UIRoot;
using _Project.Code.UI.Window;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.UIFactory
{

    
    public class UIFactory : IWindowFactory, IWindowOpener, IUIRootUser
    {
        private readonly IConfigProvider _configProvider;

        private Transform _root;
        private DiContainer _container;

        public UIFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void Initialize(IUIRoot uiRoot)
        {
            _container = uiRoot?.Container ?? throw new ArgumentNullException();
            _root = uiRoot.Transform;
        }

        public void Cleanup()
        {
            _root = null;
            _container = null;
        }

        public void CreateWindow(WindowId id)
        {
            WindowConfig config = _configProvider.ForWindow(id).Value;

            if (_container == null)
                throw new NullReferenceException("Container is not initialized");
            
            _container.InstantiatePrefab(config.Window, _root);
        }

        public void Open(WindowId id) => CreateWindow(id);
    }
}