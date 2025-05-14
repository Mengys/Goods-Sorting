using System.Collections.Generic;
using _Project.Code.Data.Dynamic;
using _Project.Code.Data.Static.Grid;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.Factories.Item;
using _Project.Code.Services.Factories.Shelf;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.Factories.Grid
{
    public class GridFactory
    {
        private readonly IUIRoot _uiRoot;
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _container;

        private readonly ItemFactory _itemFactory;
        private readonly ShelfFactory _shelfFactory;

        public GridFactory(
            IConfigProvider configProvider,
            IUIRoot uiRoot, 
            DiContainer container,
            ShelfFactory shelfFactory,
            ItemFactory itemFactory)
        {
            _configProvider = configProvider;
            _uiRoot = uiRoot;
            _container = container;

            _itemFactory = itemFactory;
            _shelfFactory = shelfFactory;
        }

        public Gameplay.GridFeature.ItemGrid Create(GridConfig config)
        {
            _configProvider.ValidateIds(config);

            List<ShelfPresenter> shelfPresenters = CreateShelves(config);
            Dictionary<CellGridPosition, ItemPresenter> mappedItems = CreateItems(config);

            return new Gameplay.GridFeature.ItemGrid(shelfPresenters, mappedItems, _uiRoot, _itemFactory);
        }

        private Dictionary<CellGridPosition, ItemPresenter> CreateItems(GridConfig config)
        {
            Dictionary<CellGridPosition, ItemPresenter> result = new();
            Dictionary<CellGridPosition, ItemId> items = config.MappedItems;

            foreach (var (position, id) in items) 
                result.Add(position, _itemFactory.Create(id, Vector3.zero));

            return result;
        }

        private List<ShelfPresenter> CreateShelves(GridConfig config)
        {
            List<ShelfPresenter> result = new();

            for (int i = 0; i < config.ShelvesCount; i++)
            {
                var view = CreateShelfView(config, i);
                var presenter = CreateShelfPresenter(config, i, view);

                result.Add(presenter);
            }

            return result;
        }

        private ShelfView CreateShelfView(GridConfig config, int index)
        {
            var id = config.GetShelfId(index);
            var position = config.GetShelfPosition(index);

            var view = _shelfFactory.Create(id, position, _uiRoot.Transform);
            return view;
        }

        private ShelfPresenter CreateShelfPresenter(GridConfig config, int index, ShelfView view)
        {
            int layers = config.GetShelfLayersCount(index);
            int columns = config.GetShelfColumnsCount(index);

            var presenter = new ShelfPresenter(view, layers, columns);
            return presenter;
        }
    }
}