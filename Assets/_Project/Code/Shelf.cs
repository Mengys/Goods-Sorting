using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    private List<Cell> _cells;

    public List<Cell> Cells => _cells;

    private void Awake()
    {
        _cells = new List<Cell>(GetComponentsInChildren<Cell>());

        foreach (var cell in _cells)
        {
            cell.Init(this);
        }
    }

    public void CheckMatches()
    {
        if (_cells.Count == 0) return;

        foreach (var cell in _cells)
        {
            if (!cell.IsBusy)
                return;
        }

        TypeSubject typeToMatch = _cells[0].Subject.SubjectType;

        foreach (var cell in _cells)
        {
            if (cell.Subject.SubjectType != typeToMatch)
                return;
        }

        DestroyCells();
    }

    private void DestroyCells()
    {
        foreach (var cell in _cells)
        {
            cell.Subject.gameObject.SetActive(false);
            cell.ToFree();
        }
    }
}
