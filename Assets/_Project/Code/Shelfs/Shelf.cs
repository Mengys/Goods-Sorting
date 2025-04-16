using System;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    private readonly int RewardMatch = 3;

    private List<Cell> _cells;
    private bool _isMadeFirstMove = false;

    public List<Cell> Cells => _cells;

    public event Action<int> Matches;

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
        if (_cells.Count == 0)
            return;

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
        Matches?.Invoke(RewardMatch);
    }

    private void DestroyCells()
    {
        foreach (var cell in _cells)
        {
            cell.Subject.gameObject.SetActive(false);
            cell.Subject.Deactivate();
            cell.ToFree();
        }
    }
}