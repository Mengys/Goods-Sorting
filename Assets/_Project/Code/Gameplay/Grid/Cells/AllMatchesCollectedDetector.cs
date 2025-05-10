using System;
using System.Linq;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.Grid.Cells
{
    public class AllMatchesCollectedDetector : IDisposable
    {
        private readonly CellsInventory _cellsInventory;
        private readonly Subject<Unit> _collected = new();

        private readonly CompositeDisposable _disposable = new();

        public AllMatchesCollectedDetector(CellsInventory cellsInventory)
        {
            _cellsInventory = cellsInventory;
            _cellsInventory.ChangedObservable.Subscribe(_ => OnCellsChanged()).AddTo(_disposable);
        }

        public Observable<Unit> Collected => _collected;

        private void OnCellsChanged()
        {
            var cells = _cellsInventory.Cells;

            var shelves = cells.Keys
                .GroupBy(p => p.Shelf);

            int minColumnsCount = int.MaxValue;

            foreach (var shelf in shelves)
            {
                var layers = shelf.GroupBy(p => p.Layer);

                var columnsCount = layers.First().Count();

                minColumnsCount = Mathf.Min(minColumnsCount, columnsCount);
            }

            var groupedById = _cellsInventory
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

            var maxItemsCount = groupedById.Values.Max(l => l.Count);

            if (maxItemsCount >= minColumnsCount)
                return;

            _collected.OnNext(Unit.Default);
        }

        public void Dispose() =>
            _disposable.Dispose();
    }
}