using System.Collections.Generic;
using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Shelfs;
using _Project.Code.Gameplay.Shelfs.Cells;
using _Project.Code.Gameplay.Subjects;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.Boosters.Boosters
{
    public class ReplaceObjects : IAbility
    {
        private List<Shelfs.Shelf> _shelves;
        private int _countObjectsReplace;
    
        public ReplaceObjects(List<Shelfs.Shelf> shelves, int countObjectsReplace)
        {
            _countObjectsReplace = countObjectsReplace;
        }

        public void Use()
        {
            StartReplaceObjects(_shelves);
        }

        public void Initialize(DiContainer container)
        {
            _shelves = container.Resolve<List<Shelfs.Shelf>>();
        }

        public void StartReplaceObjects(List<Shelfs.Shelf> shelves)
        {
            List<Subject> allActiveSubjects = new List<Subject>();

            foreach (Shelfs.Shelf shelf in shelves)
            {
                foreach (Shelfs.Cells.Cell cell in shelf.Cells)
                {
                    if (cell.Subject != null && cell.Subject.IsActive)
                    {
                        allActiveSubjects.Add(cell.Subject);
                    }
                }
            }

            if (allActiveSubjects.Count < _countObjectsReplace)
            {
                return;
            }

            TypeSubject newType = (TypeSubject)Random.Range(0, System.Enum.GetValues(typeof(TypeSubject)).Length);

            Shuffle(allActiveSubjects);
            List<Subject> selectedSubjects = allActiveSubjects.GetRange(0, _countObjectsReplace);

            foreach (Subject subject in selectedSubjects)
            {
                SetSubjectType(subject, newType);
            }
        }


        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        private void SetSubjectType(Subject subject, TypeSubject newType)
        {
            subject.SetSubjectType(newType);
            subject.SubjectViev.SetDisplay();
        }
    }
}