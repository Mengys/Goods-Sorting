using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Grid.Cells;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Services.ConfigProvider;
using UnityEngine;

namespace _Project.Code.Gameplay.Grid.Config
{
    [Serializable]
    public struct GridConfig
    {
        private Dictionary<int, ShelfId> _shelves;
        private Dictionary<int, Vector2> _positions;
        private Dictionary<int, ItemGrid> _itemGrids;
    
        private Dictionary<CellGridPosition, ItemId> _mappedItems;

        public GridConfig(
            Dictionary<int, ShelfId> shelves,
            Dictionary<int, Vector2> positions,
            Dictionary<int, ItemGrid> itemGrids)
        {
            _itemGrids = itemGrids;
            _positions = positions;
            _shelves = shelves;
            
            _mappedItems = ConfigAdapter.AsMappedItems(itemGrids);
        }

        public int ShelvesCount => _shelves.Count;

        public ShelfId GetShelfId(int index) =>
            _shelves[index];

        public Vector2 GetShelfPosition(int index) =>
            _positions[index];

        public int GetShelfLayersCount(int index) =>
            _itemGrids[index].LayersCount;

        public int GetShelfColumnsCount(int index) =>
            _itemGrids[index].ColumnsCount;

        public ItemGrid GetItemGrid(int index) =>
            _itemGrids[index];

        public Dictionary<CellGridPosition, ItemId> MappedItems => 
            new(_mappedItems);
    }

    public struct ItemGrid
    {
        public List<ItemId?> Items;

        public int LayersCount;
        public int ColumnsCount;

        public ItemId? GetItem(int layer, int column) => 
            Items[layer * ColumnsCount + column];

        public List<ItemId?> GetItemsOnLayer(int index) => 
            Items.GetRange(index * ColumnsCount, ColumnsCount);
    }
}