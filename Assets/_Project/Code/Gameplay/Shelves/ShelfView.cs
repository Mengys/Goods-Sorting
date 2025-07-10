using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Gameplay.Shelves
{
    public class ShelfView : MonoBehaviour
    {
        [SerializeField] private Transform _cells;
        [SerializeField] private bool _reversedCellOrder = false;

        private Transform _itemsHolder;
        private List<Transform> _layers = new();
        private RectTransform _rectTransform;

        public int ColumnsCount => FirstLayerCells.Count;

        public Vector2 Position
        {
            get => _rectTransform.anchoredPosition;
            set => _rectTransform.anchoredPosition = value;
        }

        private Transform ItemsHolder => GetItemsHolder();
        private List<Transform> FirstLayerCells => GetFirstLayerCells();

        public void Initialize() =>
            _rectTransform = GetComponent<RectTransform>();

        public void PlaceItem(Transform item, int layer, int column)
        {
            if (!IsValidColumnIndex(column, out var exception))
                throw exception;
            item.transform.SetParent(GetLayer(layer), false);
            item.transform.position = GetCellPosition(column, layer);
        }

        public void MoveItem(Transform item, int layer, int column) {
            if (!IsValidColumnIndex(column, out var exception))
                throw exception;
            DOTween.Kill(item);
            var pos = item.position;
            item.transform.SetParent(GetLayer(layer), false);
            item.transform.localScale = Vector3.one;
            item.transform.position = pos;

            item.transform.DOMove(GetCellPosition(column, layer), 0.2f);
            DOVirtual.DelayedCall(0.2f, () => item.transform.DOScaleY(item.transform.localScale.y * 0.8f, 0.1f).SetEase(Ease.InOutSine).SetLoops(2,LoopType.Yoyo));
            //DOVirtual.DelayedCall(0.2f, () => {
            //    item.transform.SetParent(GetLayer(layer),false);
            //    item.transform.position = GetCellPosition(column, layer);
            //});
        }

        public Vector3 GetCellPosition(int column, int layer)
        {
            if (!IsValidColumnIndex(column, out var exception))
                throw exception;

            return FirstLayerCells[column].position;
        }

        private bool IsValidColumnIndex(int index, out Exception exception)
        {
            bool isValid = index >= 0 || index < ColumnsCount;

            exception = isValid
                ? null
                : new IndexOutOfRangeException($"Column {index} is out of range");

            return isValid;
        }

        private List<Transform> GetFirstLayerCells()
        {
            var cells = _cells?.GetComponentsInChildren<Transform>()
                .Where(t => t != _cells.transform)
                .ToList();

            if (_reversedCellOrder)
                cells?.Reverse();

            return cells;
        }

        private Transform GetItemsHolder()
        {
            if (_itemsHolder != null) return _itemsHolder;

            _itemsHolder = new GameObject("Items", typeof(RectTransform)).transform;
            
            _itemsHolder.SetParent(transform, false);

            var rect = _itemsHolder.GetComponent<RectTransform>();
            
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            
            return _itemsHolder;
        }

        private Transform GetLayer(int index)
        {
            while (_layers.Count <= index)
            {
                var layer = new GameObject($"Layer {_layers.Count}", typeof(RectTransform)).transform;

                layer.SetParent(ItemsHolder, false);
                layer.SetSiblingIndex(0);
                
                var rect = layer.GetComponent<RectTransform>();
                
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;

                _layers.Add(layer);
            }

            return _layers[index];
        }
    }
}