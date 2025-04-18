using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ReplaceObjects : IAbility
{
    private List<Shelf> _shelves;
    private int _countObjectsReplace;
    
    public ReplaceObjects(List<Shelf> shelves, int countObjectsReplace)
    {
        _countObjectsReplace = countObjectsReplace;
    }

    public void Use()
    {
        StartReplaceObjects(_shelves);
    }

    public void Initialize(DiContainer container)
    {
        _shelves = container.Resolve<List<Shelf>>();
    }

    public void StartReplaceObjects(List<Shelf> shelves)
    {
        List<Subject> allActiveSubjects = new List<Subject>();

        foreach (Shelf shelf in shelves)
        {
            foreach (Cell cell in shelf.Cells)
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