using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BostersInitializeButtons : MonoBehaviour
{
    [SerializeField] private Button _stopTimerButton;
    [SerializeField] private Button _collectingPairButton;
    [SerializeField] private Button _replaceObjectsButton;
    [SerializeField] private Button _shuffleButton;
    [SerializeField] private Button _bombButton;

    private void OnEnable()
    {
        //_stopTimerButton.onClick.AddListener(PauseTime);
        //_collectingPairButton.onClick.AddListener(CollectPairs);
        //_replaceObjectsButton.onClick.AddListener(Replace);
        //_shuffleButton.onClick.AddListener(Shuffle);
        //_bombButton.onClick.AddListener(BombActivate);
    }

    private void OnDisable()
    {
        //_stopTimerButton.onClick.RemoveListener(PauseTime);
        //_collectingPairButton.onClick.RemoveListener(CollectPairs);
        //_replaceObjectsButton.onClick.RemoveListener(Replace);
        //_shuffleButton.onClick.RemoveListener(Shuffle);
        //_bombButton.onClick.RemoveListener(BombActivate);
    } 
}