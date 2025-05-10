using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Shelves;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.Grid.Cells
{
    public class CellsInventory
    {
        private event Action Changed;

        private readonly List<ShelfPresenter> _shelves;
        private readonly Dictionary<CellGridPosition, ItemPresenter> _cells;

        public CellsInventory(List<ShelfPresenter> shelves)
        {
            _shelves = shelves;

            _cells = new Dictionary<CellGridPosition, ItemPresenter>();

            InitializeCells(shelves);
        }

        public Observable<Unit> ChangedObservable => Observable.FromEvent(
                action => Changed += action,
                action => Changed -= action)
            .ThrottleFirstLast(TimeSpan.FromMilliseconds(100));

        public IReadOnlyDictionary<CellGridPosition, ItemPresenter> Cells => _cells;

        public bool IsCellFree(CellGridPosition position) =>
            !_cells.TryGetValue(position, out var item) || item == null;

        public void Set(CellGridPosition position, ItemPresenter item)
        {
            _cells[position] = item;

            if (_shelves[position.Shelf].ColumnsCount <= position.Column) return;

            if (item != null)
            {
                _shelves[position.Shelf].PlaceItem(item.Transform, position.Column, position.Layer);

                item.SetActive(position.Layer == 0);
            }

            Changed?.Invoke();
        }

        public void Clear(CellGridPosition position)
        {
            var item = Get(position);

            if (item == null) return;

            item.Destroy();

            _cells[position] = null;

            Changed?.Invoke();
        }

        public ItemPresenter Pop(CellGridPosition position)
        {
            var item = _cells[position];

            _cells[position] = null;

            Changed?.Invoke();

            return item;
        }

        public void Move(CellGridPosition from, CellGridPosition to)
        {
            var item = _cells[from];

            _cells[from] = null;

            Set(to, item);
        }

        public ItemPresenter Get(CellGridPosition position) =>
            _cells.GetValueOrDefault(position);

        public CellGridPosition? GetCellPositionWith(ItemPresenter item)
        {
            if (_cells.ContainsValue(item))
                return _cells.First(x => x.Value == item).Key;

            return null;
        }

        public CellGridPosition? GetClosestCellPosition(Vector3 point)
        {
            Dictionary<CellGridPosition, Vector3> cells = new();

            for (int i = 0; i < _shelves.Count; i++)
            {
                var shelf = _shelves[i];

                for (int j = 0; j < shelf.ColumnsCount; j++)
                {
                    var cell = shelf.GetCellPosition(j);

                    if (cell == null) continue;

                    var cellPosition = new CellGridPosition(i, 0, j);
                    cells[cellPosition] = cell.Value;
                }
            }

            KeyValuePair<CellGridPosition, Vector3>? closest = null;
            var minDist = float.MaxValue;

            foreach (var pair in cells)
            {
                var dist = Vector3.Distance(point, pair.Value);

                if (dist < minDist)
                {
                    minDist = dist;
                    closest = pair;
                }
            }

            return closest?.Key;
        }

        public Vector3? GetCellTransformPosition(CellGridPosition position)
        {
            return position.Shelf >= _shelves.Count
                ? null
                : _shelves[position.Shelf].GetCellPosition(position.Column, position.Layer);
        }

        public bool IsActive(ItemPresenter item) =>
            GetCellPositionWith(item) is { Layer: 0 };

        private void InitializeCells(List<ShelfPresenter> shelves)
        {
            for (var shelf = 0; shelf < shelves.Count; shelf++)
            {
                var columnsCount = shelves[shelf].ColumnsCount;
                var layersCount = shelves[shelf].LayersCount;

                for (int column = 0; column < columnsCount; column++)
                {
                    for (int layer = 0; layer < layersCount; layer++)
                    {
                        var position = new CellGridPosition(shelf, layer, column);
                        _cells[position] = null;
                    }
                }
            }
        }
    }
}