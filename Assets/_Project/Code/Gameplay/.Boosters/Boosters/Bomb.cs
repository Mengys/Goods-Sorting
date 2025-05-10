using System.Collections.Generic;
using _Project.Code.Gameplay.Boosters.Ability;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.Boosters.Boosters
{
    public class Bomb : IAbility
    {
        private List<Shelfs.Shelf> _shelves;

        public void Initialize(DiContainer container)
        {
            _shelves = container.Resolve<List<Shelfs.Shelf>>();
        }

        public void Use()
        {
            Activate(_shelves);
        }

        private void Activate(List<Shelfs.Shelf> shelves)
        {
            List<Subject<>> allSubjects = new List<Subject>();

            foreach (Shelfs.Shelf shelf in shelves)
            {
                foreach (Shelfs.Cells.Cell cell in shelf.Cells)
                {
                    if (cell.Subject != null && cell.Subject.IsActive)
                    {
                        allSubjects.Add(cell.Subject);
                    }
                }
            }

            if (allSubjects.Count == 0)
                return;

            TypeSubject randomType = allSubjects[Random.Range(0, allSubjects.Count)].SubjectType;

            foreach (Subject subject in allSubjects)
            {
                if (subject.SubjectType == randomType)
                {
                    Shelfs.Cells.Cell currentCell = subject.CurrentCell;

                    if (currentCell != null)
                    {
                        currentCell.ToFree();
                    }

                    subject.Deactivate();
                    subject.gameObject.SetActive(false);
                }
            }
        }
    }
}