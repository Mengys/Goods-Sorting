using System.Collections.Generic;
using _Project.Code.Gameplay.Grid.Cells;
using _Project.Code.Gameplay.Grid.Config;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.Factories.Item;
using _Project.Code.Services.Factories.Shelf;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.Factories.Grid
{
    //Must be refactored as Grid view factory
    public class PreviewGridFactory
    {
        private readonly IConfigProvider _configProvider;

        private readonly ShelfFactory _shelfFactory;
        private readonly ItemFactory _itemFactory;

        public PreviewGridFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
            _itemFactory = new ItemFactory(configProvider, new DiContainer());
            _shelfFactory = new ShelfFactory(configProvider, new DiContainer());
        }

        public List<ShelfView> Create(GridConfig config, Transform parent)
        {
            _configProvider.ValidateIds(config);

            List<ShelfView> shelves = CreateShelves(config, parent);
            Dictionary<CellGridPosition, ItemView> items = CreateItems(config);

            PlaceItems(items, shelves);

            return shelves;
        }

        private Dictionary<CellGridPosition, ItemView> CreateItems(GridConfig config)
        {
            Dictionary<CellGridPosition, ItemView> result = new();
            Dictionary<CellGridPosition, ItemId> items = config.MappedItems;

            foreach (var (position, id) in items)
                result.Add(position, _itemFactory.CreatePreview(id, Vector3.zero));

            return result;
        }

        private void PlaceItems(Dictionary<CellGridPosition, ItemView> items, List<ShelfView> shelves)
        {
            foreach (var (position, itemView) in items)
                shelves[position.Shelf].PlaceItem(itemView.transform, position.Layer, position.Column);
        }

        private List<ShelfView> CreateShelves(GridConfig config, Transform parent)
        {
            List<ShelfView> result = new();

            for (int i = 0; i < config.ShelvesCount; i++)
                result.Add(CreateShelfView(config, i, parent));

            return result;
        }

        private ShelfView CreateShelfView(GridConfig config, int index, Transform parent)
        {
            var id = config.GetShelfId(index);
            var position = config.GetShelfPosition(index);

            var view = _shelfFactory.Create(id, position, parent);

            return view;
        }
    }
}