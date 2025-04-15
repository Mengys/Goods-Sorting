using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector3 _startPosition;
    private Transform _startParent;
    private Subject _subject;
    private Cell _startCell;
    private Canvas _originalCanvas; 
    private Canvas _topCanvas; 

    private void Awake()
    {
        _subject = GetComponent<Subject>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _startParent = transform.parent;
        _startCell = _subject.CurrentCell;

        _originalCanvas = transform.GetComponentInParent<Canvas>();

        if (_startCell != null)
        {
            _startCell.ToFree();
            _subject.ToFree();
        }

        _topCanvas = FindTopCanvas();

        transform.SetParent(_topCanvas.transform);
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

        transform.SetParent(_originalCanvas.transform);
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

    private Canvas FindTopCanvas()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        Canvas topCanvas = null;
        int maxSortingOrder = int.MinValue;

        foreach (Canvas canvas in canvases)
        {
            if (canvas.sortingOrder > maxSortingOrder)
            {
                topCanvas = canvas;
                maxSortingOrder = canvas.sortingOrder;
            }
        }

        return topCanvas;
    }
}