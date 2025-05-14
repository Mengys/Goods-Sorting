using System;
using _Project.Code.Data.Dynamic;
using _Project.Code.Gameplay.Items;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Code.Gameplay.GridFeature.Services
{
    public class ItemsDragHandler 
    {
        private CellGridPosition _startCellPosition;
        private bool _isDraggingItem;

        private readonly Transform _dragItemHolder;
        private readonly ItemInventory _itemInventory;
        private readonly Subject<Unit> _draggedItem = new();
        
        private float _snapThreshold = 100;
        private Vector2 _dragOffset = Vector2.up * 30;

        public ItemsDragHandler(
            Transform dragItemHolder,
            ItemInventory itemInventory)
        {
            _itemInventory = itemInventory;
            _dragItemHolder = dragItemHolder;
        }

        public bool Enabled { get; set; } = true;

        public Observable<Unit> DraggedItem => _draggedItem;

        public void Configure(float snapThreshold, Vector2 dragOffset)
        {
            _snapThreshold = snapThreshold;
            _dragOffset = dragOffset;
        }

        public IDisposable Register(ItemPresenter item)
        {
            CompositeDisposable disposable = new();
            
            var events = item.DragAndDropEvents;

            events.DragBegan
                .Subscribe(data => OnBegin(data, item))
                .AddTo(item.Transform)
                .AddTo(disposable);

            events.Dragged
                .Subscribe(data => OnDrag(data, item))
                .AddTo(item.Transform)
                .AddTo(disposable);

            events.DragEnded.Subscribe(data => OnEnd(data, item))
                .AddTo(item.Transform)
                .AddTo(disposable);

            return disposable;
        }

        private bool CanPlace(ItemPresenter item, CellGridPosition to) =>
            IsWithinThreshold(to, item) && _itemInventory.IsCellFree(to);

        private bool IsWithinThreshold(CellGridPosition cellGridPosition, ItemPresenter item)
        {
            var cellPosition = _itemInventory.GetCellTransformPosition(cellGridPosition);

            if (cellPosition == null) return false;

            return Vector3.Distance(item.Transform.position, cellPosition.Value) < _snapThreshold;
        }

        private void OnBegin(PointerEventData data, ItemPresenter item)
        {
            if (!Enabled) return;

            if (_isDraggingItem || !_itemInventory.IsActive(item)) return;

            _isDraggingItem = true;

            var position = _itemInventory.GetCellPositionWith(item);

            if (position == null) return;

            _startCellPosition = position.Value;

            item.Transform.SetParent(_dragItemHolder);
            item.Transform.position = data.position + _dragOffset;

            _draggedItem.OnNext(Unit.Default);
        }

        private void OnDrag(PointerEventData data, ItemPresenter item)
        {
            if (!_isDraggingItem) return;

            item.Transform.position = data.position + _dragOffset;
        }

        private void OnEnd(PointerEventData data, ItemPresenter item)
        {
            if (!_isDraggingItem) return;

            _isDraggingItem = false;

            CellGridPosition targetCellPosition = _startCellPosition;
            CellGridPosition? closestCellPosition = _itemInventory.GetClosestCellPosition(item.Transform.position);

            if (closestCellPosition != null && CanPlace(item, closestCellPosition.Value))
                targetCellPosition = closestCellPosition.Value;

            if (targetCellPosition.Equals(_startCellPosition))
                _itemInventory.Set(targetCellPosition, item);
            else
                _itemInventory.Move(_startCellPosition, targetCellPosition);
        }
    }
}