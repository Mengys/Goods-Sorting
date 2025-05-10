using _Project.Code.Gameplay.Levels.Configs;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.Factories.Grid;
using _Project.Code.Services.Factories.UI.WindowFactory;
using Zenject;

namespace _Project.Code.Services.Factories.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly GridFactory _gridFactory;
        private readonly DiContainer _container;

        public LevelFactory(IConfigProvider configProvider, IUIRoot uiRoot, DiContainer container)
        {
            _container = container;
            _configProvider = configProvider;
            _gridFactory = new GridFactory(configProvider, uiRoot, container);
        }

        public Level Generate(int levelIndex)
        {
            LevelConfig config = _configProvider.ForLevel(levelIndex).Value;

            var grid = _gridFactory.Create(config.Grid);
            var windowFactory = _container.Resolve<IWindowFactory>();
            var timer = _container.Resolve<Timer>();

            return new Level(grid, windowFactory, timer);
        }
    }
}