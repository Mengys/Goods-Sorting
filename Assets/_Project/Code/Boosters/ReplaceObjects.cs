using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceObjects : MonoBehaviour
{
    private readonly int CountObjectsReplace = 6;

    [SerializeField] private int _prise = 3;

    public int Prise => _prise;

    public void StartReplaceObjects(List<Shelf> shelves)
    {
        List<Subject> allActiveSubjects = new List<Subject>();

        // �������� ��� �������� Subject
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

        // ���� �������� �������� ������ 6 � ������ �� ������
        if (allActiveSubjects.Count < 6)
            return;

        // �������� ��������� ���
        TypeSubject newType = (TypeSubject)Random.Range(0, System.Enum.GetValues(typeof(TypeSubject)).Length);

        // ������������ ������ � ���� ������ 6
        Shuffle(allActiveSubjects);
        List<Subject> selectedSubjects = allActiveSubjects.GetRange(0, CountObjectsReplace);

        // ������ ��� � ���������
        foreach (Subject subject in selectedSubjects)
        {
            SetSubjectType(subject, newType);
        }
    }

    // ��������������� ������� ��� ������������� ������
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
    }
}