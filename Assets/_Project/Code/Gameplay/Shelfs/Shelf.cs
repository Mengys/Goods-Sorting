using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Shelfs.Cells;
using _Project.Code.Gameplay.Subjects;
using UnityEngine;

namespace _Project.Code.Gameplay.Shelfs
{
    public class Shelf : MonoBehaviour
    {
        private readonly int RewardMatch = 3;

        private List<Cell> _cells;

        public List<Cell> Cells => _cells;

        public event Action<int> Matched;

        private void Awake()
        {
            _cells = new List<Cell>(GetComponentsInChildren<Cell>());

            foreach (var cell in _cells)
            {
                cell.Init(this);
            }
        }

        public void UnsubscribeAll()
        {
            Matched = null;
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
            Matched?.Invoke(RewardMatch);
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
}