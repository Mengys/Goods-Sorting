using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int _prise = 3;

    public int Prise => _prise;

    public void Activate(List<Shelf> shelves)
    {
        List<Subject> allSubjects = new List<Subject>();

        foreach (Shelf shelf in shelves)
        {
            foreach (Cell cell in shelf.Cells)
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
                Cell currentCell = subject.CurrentCell;

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