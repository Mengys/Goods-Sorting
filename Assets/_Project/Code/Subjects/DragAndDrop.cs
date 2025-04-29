using System.Collections.Generic;
using _Project.Code.Shelfs.Cells;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Code.Subjects
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Vector3 _startPosition;
        private Transform _startParent;
        private Subject _subject;
        private Cell _startCell;
        private Transform _layerDrage;

        private void Awake()
        {
            _subject = GetComponent<Subject>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GameController.OnFirstMove();

            _startPosition = transform.position;
            _startParent = transform.parent;
            _startCell = _subject.CurrentCell;

            if (_startCell != null)
            {
                _startCell.ToFree();
                _subject.ToFree();
            }

            transform.SetParent(_layerDrage.transform);
            transform.SetAsLastSibling();  
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Cell targetCell = GetCellUnderPointer(eventData);

            if (targetCell != null && !targetCell.IsBusy)
            {
                transform.SetParent(targetCell.transform);
                transform.localPosition = Vector3.zero;
                targetCell.GetSubject(_subject);
            }
            else
            {
                transform.SetParent(_startParent);
                transform.position = _startPosition;

                if (_startCell != null)
                {
                    _startCell.GetSubject(_subject);
                }
            }
        }

        public void InitializeLayerDrage(Transform layerTransform)
        {
            _layerDrage = layerTransform;
        }

        private Cell GetCellUnderPointer(PointerEventData eventData)
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            foreach (var result in raycastResults)
            {
                Cell cell = result.gameObject.GetComponent<Cell>();

                if (cell != null)
                    return cell;
            }

            return null;
        }
    }
}