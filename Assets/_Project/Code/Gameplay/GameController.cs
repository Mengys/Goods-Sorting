using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Shelfs;
using _Project.Code.Gameplay.Shelfs.Cells;
using UnityEngine;

namespace _Project.Code.Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private List<Shelf> _shelves = new List<Shelf>();
        [SerializeField] private SpawnerSubjects _spawnerSubjects;
        [SerializeField] private Transform _layerDragging;

        private static bool _hasFirstMoveHappened = false;

        public static event Action FirstMoveMade;

        private void Start()
        {
            _spawnerSubjects.SpawnSubjects(_shelves);

            foreach (Shelf shelf in _shelves)
            {
                foreach (Cell cell in shelf.Cells)
                {
                    if (cell.Subject != null)
                    {
                        cell.Subject.DragAndDrop.InitializeLayerDrage(_layerDragging);
                        cell.Subject.SubjectViev.SetDisplay();
                    }
                }
            }
        }

        public static void OnFirstMove()
        {
            if (_hasFirstMoveHappened)
                return;

            _hasFirstMoveHappened = true;
            FirstMoveMade?.Invoke();
        }
    }
}