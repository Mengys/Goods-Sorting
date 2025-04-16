using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Shelf> _shelves = new List<Shelf>();
    [SerializeField] private SpawnerSubjects _spawnerSubjects;
    [SerializeField] private Money _money;
    [SerializeField] private Timer _timer;
    [SerializeField] private Transform _layerDragging;
    [SerializeField] private Image _gameOver;

    private static bool _hasFirstMoveHappened = false;

    public static event Action FirstMoveMade;

    private void OnEnable()
    {
        _timer.Ended += ShoveGameOver;
    }

    private void OnDisable()
    {
        _timer.Ended -= ShoveGameOver;
    }

    private void Awake()
    {
        _gameOver.gameObject.SetActive(false);
    }

    private void Start()
    {
        _spawnerSubjects.SpawnSubjects(_shelves);

        foreach (Shelf shelf in _shelves)
        {
            shelf.Matches += _money.AddMoney;

            foreach (Cell cell in shelf.Cells)
            {
                if (cell.Subject != null)
                {
                    cell.Subject.DragAndDrop.InitializeLayerDrage(_layerDragging);
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

    private void ShoveGameOver()
    {
        _gameOver.gameObject.SetActive(true);
    }
}
