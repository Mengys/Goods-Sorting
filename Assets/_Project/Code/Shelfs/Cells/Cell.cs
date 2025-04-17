using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool _isBusy;
    private Subject _subject;
    private Shelf _parentShelf;

    public bool IsBusy => _isBusy;
    public Subject Subject => _subject;

    public void Init(Shelf shelf)
    {
        _parentShelf = shelf;
    }

    public void GetSubject(Subject subject)
    {
        _subject = subject;
        _isBusy = true;
        subject.GetCell(this);

        _parentShelf?.CheckMatches();
    }

    public void ToFree()
    {
        if (_subject != null)
        {
            _subject.ToFree();
        }

        _subject = null;
        _isBusy = false;

        _parentShelf?.CheckMatches();
    }
}