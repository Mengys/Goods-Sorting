using UnityEngine;

public class Subject : MonoBehaviour
{
    [SerializeField] private TypeSubject _typeSubject;
    [SerializeField] private DragAndDrop _dragAndDrop;

    private Cell _currentCell;
    private int _defaultSiblingIndex;
    //private DragAndDrop _dragAndDrop;

    public Cell CurrentCell => _currentCell;
    public TypeSubject SubjectType => _typeSubject;
    public DragAndDrop DragAndDrop => _dragAndDrop;

    public void GetCell(Cell cell)
    {
        _currentCell = cell;
    }

    public void ToFree()
    {
        _currentCell = null;
    }
}