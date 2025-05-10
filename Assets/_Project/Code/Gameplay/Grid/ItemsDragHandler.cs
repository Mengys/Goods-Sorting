using System;
using _Project.Code.Gameplay.Grid.Cells;
using _Project.Code.Gameplay.Items;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Code.Gameplay.Grid
{
    public class ItemsDragHandler : IDisposable
    {
        private CellGridPosition _startCellPosition;
        private bool _isDraggingItem;

        private readonly Transform _dragItemHolder;
        private readonly CellsInventory _cellsInventory;
        private readonly CompositeDisposable _disposable = new();
        private readonly Subject<Unit> _draggedItem = new();
        
        private bool _enabled = true;

        private const float SnapThreshold = 40f;

        public ItemsDragHandler(Transform dragItemHolder, CellsInventory cellsInventory)
        {
            _cellsInventory = cellsInventory;
            _dragItemHolder = dragItemHolder;
        }

        public Observable<Unit> DraggedItem => _draggedItem;

        public void Dispose() =>
            _disposable.Dispose();

        private Vector2 DragOffset => Vector2.up * 10;

        public void Add(ItemPresenter item)
        {
            var events = item.DragAndDropEvents;

            events.DragBegan
                .Subscribe(data => OnBegin(data, item))
                .AddTo(item.Transform)
                .AddTo(_disposable);

            events.Dragged
                .Subscribe(data => OnDrag(data, item))
                .AddTo(item.Transform)
                .AddTo(_disposable);

            events.DragEnded.Subscribe(data => OnEnd(data, item))
                .AddTo(item.Transform)
                .AddTo(_disposable);
        }

        private bool CanPlace(ItemPresenter item, CellGridPosition to) =>
            IsWithinThreshold(to, item) && _cellsInventory.IsCellFree(to);

        private bool IsWithinThreshold(CellGridPosition cellGridPosition, ItemPresenter item)
        {
            var cellPosition = _cellsInventory.GetCellTransformPosition(cellGridPosition);

            if (cellPosition == null) return false;

            return Vector3.Distance(item.Transform.position, cellPosition.Value) < SnapThreshold;
        }

        private void OnBegin(PointerEventData data, ItemPresenter item)
        {
            if (!_enabled) return;

            if (_isDraggingItem || !_cellsInventory.IsActive(item)) return;

            _isDraggingItem = true;

            var position = _cellsInventory.GetCellPositionWith(item);

            if (position == null) return;

            _startCellPosition = position.Value;

            item.Transform.SetParent(_dragItemHolder);
            item.Transform.position = data.position + DragOffset;

            _draggedItem.OnNext(Unit.Default);
        }

        private void OnDrag(PointerEventData data, ItemPresenter item)
        {
            if (!_isDraggingItem) return;

            item.Transform.position = data.position + DragOffset;
        }

        private void OnEnd(PointerEventData data, ItemPresenter item)
        {
            if (!_isDraggingItem) return;

            _isDraggingItem = false;

            CellGridPosition targetCellPosition = _startCellPosition;
            CellGridPosition? closestCellPosition = _cellsInventory.GetClosestCellPosition(item.Transform.position);

            if (closestCellPosition != null && CanPlace(item, closestCellPosition.Value))
                targetCellPosition = closestCellPosition.Value;

            if (targetCellPosition.Equals(_startCellPosition))
                _cellsInventory.Set(targetCellPosition, item);
            else
                _cellsInventory.Move(_startCellPosition, targetCellPosition);
        }

        public void Disable()
        {
            _enabled = false;
        }

        public void Enable()
        {
            _enabled = true;
        }
    }
}