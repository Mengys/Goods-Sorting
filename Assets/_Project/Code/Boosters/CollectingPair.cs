using System.Collections.Generic;
using UnityEngine;

public class CollectingPair : MonoBehaviour
{
    [SerializeField] private int _prise = 3;

    public int Prise => _prise;

    public void StartCollectPairs(List<Shelf> shelves)
    {
        Dictionary<TypeSubject, List<Subject>> subjectsByType = new Dictionary<TypeSubject, List<Subject>>();

        foreach (Shelf shelf in shelves)
        {
            foreach (Cell cell in shelf.Cells)
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

            if (subjects.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
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