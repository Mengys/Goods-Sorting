using System;
using _Project.Code.Data.Static.Windows;
using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.UI.Windows;
using _Project.Code.UI.Windows.Base;

namespace _Project.Code.Services.Factories.UI
{
    public class UIFactory : IWindowFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IUIRoot _uiRoot;

        public UIFactory(IConfigProvider configProvider, IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            _configProvider = configProvider;
        }

        public Window Create(WindowId id)
        {
            WindowConfig? config = _configProvider.ForWindow(id);

            if (config == null)
                throw new ArgumentException($"Window config for {id} not found");
            
            if (_uiRoot?.Container == null)
                throw new NullReferenceException("Container is not initialized");
            
            var window = _uiRoot.Container
                .InstantiatePrefabForComponent<Window>(config.Value.Window, _uiRoot.Transform);
            
            window.Initialize();

            return window;
        }
    }
}