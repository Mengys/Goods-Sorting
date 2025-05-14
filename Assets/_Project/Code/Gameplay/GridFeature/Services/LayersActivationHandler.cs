using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Dynamic;
using UnityEngine;

namespace _Project.Code.Gameplay.GridFeature.Services
{
    public class LayersActivationHandler
    {
        private readonly ItemInventory _itemInventory;

        public LayersActivationHandler(ItemInventory itemInventory)
        {
            _itemInventory = itemInventory;
        }

        public void Handle()
        {
            var itemPositions = _itemInventory.Cells.Keys;

            var shelfGroups =
                itemPositions.GroupBy(p => p.Shelf);

            foreach (var group in shelfGroups)
            {
                List<IGrouping<int, CellGridPosition>> layerGroups = group
                    .GroupBy(p => p.Layer)
                    .OrderBy(g => g.Key)
                    .ToList();

                if (layerGroups.Count < 2) continue;

                var firstLayer = layerGroups.ElementAt(0);

                if (IsLayerEmpty(firstLayer))
                {
                    for (int i = 1; i < layerGroups.Count; i++)
                    {
                        var layer = layerGroups.ElementAt(i);

                        if (!IsLayerEmpty(layer))
                        {
                            ReplaceLayer(firstLayer, layer);
                            break;
                        }
                    }
                }
            }
        }

        private void ReplaceLayer(
            IGrouping<int, CellGridPosition> replaceable,
            IGrouping<int, CellGridPosition> target)
        {
            var replaceablePositions = replaceable.ToList();
            var targetPositions = target.ToList();

            for (int i = 0; i < replaceablePositions.Count; i++)
            {
                var replaceablePosition = replaceablePositions[i];
                var targetPosition = targetPositions[i];

                var item = _itemInventory.Get(targetPosition);
                _itemInventory.Set(replaceablePosition, item);
                _itemInventory.Pop(targetPosition);
            }
        }

        private bool IsLayerEmpty(IGrouping<int, CellGridPosition> layerGroup)
        {
            var positions = layerGroup.ToList();

            var items = positions
                .Select(p => _itemInventory.Get(p)).ToList();

            return items.All(item => item is null);
        }
    }
}