using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Dynamic;
using _Project.Code.Data.Static.Grid;
using _Project.Code.Data.Static.Level;
using _Project.Code.Data.Static.Shelf;
using _Project.Code.Gameplay.Items;
using UnityEngine;

namespace _Project.Code.Data.Services
{
    public static class AssetDataFormatter
    {
        public static LevelConfig AsLevelConfig(LevelConfigAsset asset) =>
            new()
            {
                Difficulty = asset.Difficulty,
                Grid = AsGridConfig(asset.Grid)
            };

        public static GridConfig AsGridConfig(GridConfigAsset asset)
        {
            Dictionary<int, ItemGrid> grids = asset.Shelves
                .Select(x => x.ItemGridAssetConfig)
                .Select(AsItemGrid)
                .Select((item, index) => new { item, index })
                .ToDictionary(x => x.index, x => x.item);

            Dictionary<int, ShelfId> shelves = asset.Shelves
                .Select(x => x.Id)
                .Select(AsShelfId)
                .Select((item, index) => new { item, index })
                .ToDictionary(x => x.index, x => x.item);

            Dictionary<int, Vector2> positions = asset.Shelves
                .Select(x => x.Position)
                .Select((item, index) => new { item, index })
                .ToDictionary(x => x.index, x => x.item);

            return new GridConfig(shelves, positions, grids);
        }

        public static Dictionary<CellGridPosition, ItemId> AsMappedItems(Dictionary<int, ItemGrid> itemGrids)
        {
            var mappedItems = new Dictionary<CellGridPosition, ItemId>();

            foreach (var pair in itemGrids)
            {
                int shelf = pair.Key;
                ItemGrid grid = pair.Value;

                for (int layer = 0; layer < grid.LayersCount; layer++)
                {
                    for (int column = 0; column < grid.ColumnsCount; column++)
                    {
                        ItemId? itemHolder = grid.GetItem(layer, column);
                        
                        if (itemHolder.HasValue)
                        {
                            var position = new CellGridPosition(shelf, layer, column);
                            mappedItems[position] = itemHolder.Value;
                        }
                    }
                }
            }

            return mappedItems;
        }

        private static ItemGrid AsItemGrid(ItemGridAssetConfig assetConfig) =>
            new()
            {
                Items = assetConfig.Cells
                    ?.Select(strId =>
                        string.IsNullOrEmpty(strId)
                            ? null
                            : new ItemId?(new ItemId(strId)))
                    .ToList(),

                LayersCount = assetConfig.Rows,
                ColumnsCount = assetConfig.Columns
            };

        private static ShelfId AsShelfId(string id) => new(id);
    }
}