using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Code.Gameplay.DragAndDrop
{
    public class MyDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDragAndDropEvents
    {
        private readonly Subject<PointerEventData> _dragBegan = new();
        private readonly Subject<PointerEventData> _dragged = new();
        private readonly Subject<PointerEventData> _dragEnded = new();

        public Observable<PointerEventData> DragBegan => _dragBegan;
        public Observable<PointerEventData> Dragged => _dragged;
        public Observable<PointerEventData> DragEnded => _dragEnded;

        private void Awake()
        {
            _dragBegan.AddTo(this);
            _dragged.AddTo(this);
            _dragEnded.AddTo(this);
        }

        public void OnPointerDown(PointerEventData eventData) => 
            _dragBegan.OnNext(eventData);

        public void OnPointerUp(PointerEventData eventData) => 
            _dragEnded.OnNext(eventData);

        public void OnDrag(PointerEventData eventData) => 
            _dragged.OnNext(eventData);
    }
}