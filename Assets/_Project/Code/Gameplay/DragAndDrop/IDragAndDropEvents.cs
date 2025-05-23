using R3;
using UnityEngine.EventSystems;

namespace _Project.Code.Gameplay.DragAndDrop
{
    public interface IDragAndDropEvents
    {
        Observable<PointerEventData> DragBegan { get; }
        Observable<PointerEventData> Dragged { get; }
        Observable<PointerEventData> DragEnded { get; }
    }
}