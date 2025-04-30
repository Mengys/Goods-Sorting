using System.Collections.Generic;
using _Project.Code.Gameplay.Shelfs;
using _Project.Code.Gameplay.Shelfs.Cells;
using _Project.Code.Gameplay.Subjects;
using UnityEngine;
using Zenject;

public class Bomb : IAbility
{
    private List<Shelf> _shelves;

    public void Initialize(DiContainer container)
    {
        _shelves = container.Resolve<List<Shelf>>();
    }

    public void Use()
    {
        Activate(_shelves);
    }

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