using System.Collections.Generic;
using UnityEngine;

public class SpawnerSubjects : MonoBehaviour
{
    [SerializeField] private List<Subject> _prefabSubjects = new List<Subject>();
    [SerializeField] private List<Shelf> _shelves = new List<Shelf>();
    [SerializeField] private Transform _layerDragging;
    [SerializeField, Range(0f, 1f)] private float _spawnProbability = 0.7f;

    private void Start()
    {
        SpawnSubjects();
    }

    private void SpawnSubjects()
    {
        foreach (Shelf shelf in _shelves)
        {
            foreach (Cell cell in shelf.Cells)
            {
                if (cell.IsBusy) continue;

                if (Random.value < _spawnProbability)
                {
                    Subject randomSubjectPrefab = _prefabSubjects[Random.Range(0, _prefabSubjects.Count)];

                    Subject spawnedSubject = Instantiate(randomSubjectPrefab, cell.transform.position, Quaternion.identity);
                    spawnedSubject.transform.SetParent(cell.transform);

                    cell.GetSubject(spawnedSubject); 
                }
            }

            shelf.CheckMatches();
        }
    }
}
