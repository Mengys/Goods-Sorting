using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.Grid.Cells
{
    public class LayersActivator : IDisposable
    {
        private readonly CellsInventory _cellsInventory;
        private readonly CompositeDisposable _disposable = new();

        public LayersActivator(CellsInventory cellsInventory)
        {
            _cellsInventory = cellsInventory;
            _cellsInventory.ChangedObservable
                .Subscribe(_ => OnCellsChanged()).AddTo(_disposable);
        }

        private void OnCellsChanged()
        {
            var itemPositions = _cellsInventory.Cells.Keys;

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
                
                var item = _cellsInventory.Get(targetPosition);
                _cellsInventory.Set(replaceablePosition, item);
                _cellsInventory.Pop(targetPosition);
            }
        }

        private bool IsLayerEmpty(IGrouping<int, CellGridPosition> layerGroup)
        {
            var positions = layerGroup.ToList();
            
            var items = positions
                .Select(p => _cellsInventory.Get(p)).ToList();
         
            return items.All(item => item is null);
        }

        public void Dispose() => 
            _disposable.Dispose();
    }
}