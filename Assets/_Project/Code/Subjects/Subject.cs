using UnityEngine;

public class Subject : MonoBehaviour
{
    [SerializeField] private TypeSubject _typeSubject;
    [SerializeField] private DragAndDrop _dragAndDrop;
    [SerializeField] private SubjectViev _subjectViev;

    private Cell _currentCell;
    private int _defaultSiblingIndex;
    private bool _isActive = true;

    public bool IsActive => _isActive;
    public Cell CurrentCell => _currentCell;
    public TypeSubject SubjectType => _typeSubject;
    public DragAndDrop DragAndDrop => _dragAndDrop;
    public SubjectViev SubjectViev => _subjectViev;

    public void GetCell(Cell cell)
    {
        _currentCell = cell;
    }

    public void ToFree()
    {
        _currentCell = null;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public void SetSubjectType(TypeSubject newType)
    {
        _typeSubject = newType;
    }
}