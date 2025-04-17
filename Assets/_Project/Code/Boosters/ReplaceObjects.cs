using System.Collections.Generic;
using UnityEngine;

public class ReplaceObjects : MonoBehaviour
{
    private readonly int CountObjectsReplace = 6;

    [SerializeField] private int _prise = 3;

    private bool _isEnough = true;

    public int Prise => _prise;
    public bool IsEnough => _isEnough;

    public void StartReplaceObjects(List<Shelf> shelves)
    {
        _isEnough = false; 

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

        if (allActiveSubjects.Count < CountObjectsReplace)
        {
            return;
        }

        _isEnough = true;

        TypeSubject newType = (TypeSubject)Random.Range(0, System.Enum.GetValues(typeof(TypeSubject)).Length);

        Shuffle(allActiveSubjects);
        List<Subject> selectedSubjects = allActiveSubjects.GetRange(0, CountObjectsReplace);

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