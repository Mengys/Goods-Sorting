using System.Collections.Generic;
using _Project.Code.Gameplay.Shelfs;
using _Project.Code.Gameplay.Shelfs.Cells;
using _Project.Code.Gameplay.Subjects;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters
{
    public class Shuffle : MonoBehaviour
    {
        [SerializeField] private int _prise = 3;

        public int Prise => _prise;

        public void StartShuffle(List<Shelf> shelves)
        {
            List<Subject> subjects = new List<Subject>();
            List<Cell> allCells = new List<Cell>();

            foreach (Shelf shelf in shelves)
            {
                foreach (Cell cell in shelf.Cells)
                {
                    allCells.Add(cell);

                    if (cell.Subject != null && cell.Subject.IsActive)
                    {
                        subjects.Add(cell.Subject);
                    }
                }
            }

            if (subjects.Count < 2) return;

            ShuffleList(allCells);

            foreach (var subject in subjects)
            {
                subject.CurrentCell?.ToFree();
            }

            for (int i = 0; i < subjects.Count; i++)
            {
                Subject subject = subjects[i];
                Cell targetCell = allCells[i];

                if (targetCell == null) continue;

                targetCell.GetSubject(subject);
                subject.transform.SetParent(targetCell.transform);
                subject.transform.localPosition = Vector3.zero;
            }
        }

        private void ShuffleList<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}