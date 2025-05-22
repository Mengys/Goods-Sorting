using System.Linq;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.GridFeature.Services
{
    namespace _Project.Code.Gameplay.GridFeature.Services
    {
    }

    public class AllMatchesCollectedObserver
    {
        private readonly ItemInventory _itemInventory;
        private readonly Subject<Unit> _collected = new();

        public AllMatchesCollectedObserver(ItemInventory itemInventory)
        {
            _itemInventory = itemInventory;
        }

        public Observable<Unit> Collected => _collected;

        public void Observe()
        {
            var cells = _itemInventory.Cells;

            var shelves = cells.Keys
                .GroupBy(p => p.Shelf);

            int minColumnsCount = int.MaxValue;

            foreach (var shelf in shelves)
            {
                var layers = shelf.GroupBy(p => p.Layer);

                var columnsCount = layers.First().Count();

                minColumnsCount = Mathf.Min(minColumnsCount, columnsCount);
            }

            var groupedById = _itemInventory
                .Cells
                .Where(cell => cell.Value != null)
                .GroupBy(cell => cell.Value.Id)
                .ToDictionary(
                    group => group
                        .Key,
                    group => group
                        .Select(cell => cell.Key)
                        .ToList()
                );

            var maxItemsCount = groupedById.Values.Count > 0
                ? groupedById.Values.Max(l => l.Count)
                : 0;

            if (maxItemsCount >= minColumnsCount)
                return;

            _collected.OnNext(Unit.Default);
        }
    }
}