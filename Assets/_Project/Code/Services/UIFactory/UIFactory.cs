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
    public class UIFactory : IWindowFactory, IWindowOpener
    {
        private readonly IConfigProvider _configProvider;
        private readonly IUIRoot _uiRoot;

        public UIFactory(IConfigProvider configProvider, IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            _configProvider = configProvider;
        }

        public void CreateWindow(WindowId id)
        {
            WindowConfig config = _configProvider.ForWindow(id).Value;

            if (_uiRoot?.Container == null)
                throw new NullReferenceException("Container is not initialized");
            
            _uiRoot.Container.InstantiatePrefab(config.Window, _uiRoot.Transform);
        }

        public void Open(WindowId id) => CreateWindow(id);
    }
}