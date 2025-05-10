using System.Collections.Generic;
using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Shelfs;
using _Project.Code.Gameplay.Shelfs.Cells;
using _Project.Code.Gameplay.Subjects;
using Zenject;

namespace _Project.Code.Gameplay.Boosters.Boosters
{
    public class ComboBreaker : IAbility
    {
        private List<Shelfs.Shelf> _shelves;
        private int _numberObjectsSameType;

        public ComboBreaker(List<Shelfs.Shelf> shelves, int numberObjectsSameType)
        {
            _numberObjectsSameType = numberObjectsSameType;
        }

        public void Initialize(DiContainer container)
        {
            _shelves = container.Resolve<List<Shelfs.Shelf>>();
        }

        public void Use()
        {
            StartCollectPairs(_shelves);
        }

        public void StartCollectPairs(List<Shelfs.Shelf> shelves)
        {
            Dictionary<TypeSubject, List<Subject>> subjectsByType = new Dictionary<TypeSubject, List<Subject>>();

            foreach (Shelfs.Shelf shelf in shelves)
            {
                foreach (Shelfs.Cells.Cell cell in shelf.Cells)
                {
                    Subject subject = cell.Subject;

                    if (subject != null && subject.IsActive)
                    {
                        TypeSubject type = subject.SubjectType;

                        if (!subjectsByType.ContainsKey(type))
                        {
                            subjectsByType[type] = new List<Subject>();
                        }

                        subjectsByType[type].Add(subject);
                    }
                }
            }

            foreach (var pair in subjectsByType)
            {
                List<Subject> subjects = pair.Value;

                if (subjects.Count >= _numberObjectsSameType)
                {
                    for (int i = 0; i < _numberObjectsSameType; i++)
                    {
                        Subject subjectToDestroy = subjects[i];

                        subjectToDestroy.gameObject.SetActive(false);
                        subjectToDestroy.Deactivate();
                        subjectToDestroy.CurrentCell?.ToFree();
                    }

                    break;
                }
            }
        }
    }
}