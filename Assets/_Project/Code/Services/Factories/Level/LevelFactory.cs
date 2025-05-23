using System;
using System.Numerics;
using _Project.Code.Data.Static.Level;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.LevelFlow;
using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.Factories.Grid;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.Services.Factories.Item;
using _Project.Code.Services.Factories.Shelf;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace _Project.Code.Services.Factories.Level
{
    public class LevelFactory : IFactory<LevelFlow>, IFactory<ItemGrid>, IFactory<ItemId, ItemPresenter>
    {
        private readonly IConfigProvider _configProvider;
        private readonly GridFactory _gridFactory;
        private readonly DiContainer _container;
        private readonly ItemFactory _itemFactory;
        private readonly ShelfFactory _shelfFactory;

        public LevelFactory(
            IConfigProvider configProvider,
            IUIRoot uiRoot,
            DiContainer container)
        {
            _container = container;
            _configProvider = configProvider;
            
            _itemFactory = new ItemFactory(configProvider, container);
            _shelfFactory = new ShelfFactory(configProvider, container);
            _gridFactory = new GridFactory(configProvider, uiRoot, container, _shelfFactory, _itemFactory);
        }
        
        public LevelFlow Create() => 
            _container.Instantiate<LevelFlow>();

        ItemGrid IFactory<ItemGrid>.Create() => 
            _gridFactory.Create(GetLevelConfig().Grid);

        ItemPresenter IFactory<ItemId, ItemPresenter>.Create(ItemId id) => 
            _itemFactory.Create(id, Vector3.zero);

        private LevelConfig GetLevelConfig()
        {
            var levelId = _container.Resolve<IProgressProvider>().PlayerProgress.Level.Id;

            var levelConfig = _configProvider.ForLevel(levelId);

            if (!levelConfig.HasValue)
                throw new NullReferenceException(nameof(levelConfig));

            return levelConfig.Value;   
        }
    }
}