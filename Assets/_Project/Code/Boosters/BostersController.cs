using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BostersController : MonoBehaviour
{
    [SerializeField] private Button _stopTimerButton;
    [SerializeField] private Button _collectingPairButton;
    [SerializeField] private Button _replaceObjectsButton;

    [SerializeField] private StopTimer _stopTimer;
    [SerializeField] private CollectingPair _collectingPair;
    [SerializeField] private ReplaceObjects _replaceObjects;

     private Timer _timer;
     private Money _money;
    private List<Shelf> _shelfs;

    private void OnEnable()
    {
        _stopTimerButton.onClick.AddListener(PauseTime);
        _collectingPairButton.onClick.AddListener(CollectPairs);
        _replaceObjectsButton.onClick.AddListener(Replace);
    }

    private void OnDisable()
    {
        _stopTimerButton.onClick.RemoveListener(PauseTime);
        _collectingPairButton.onClick.RemoveListener(CollectPairs);
        _replaceObjectsButton.onClick.RemoveListener(Replace);
    }

    private void Start()
    {
        _stopTimer.InitialuzeTimer(_timer);
    }

    public void Initialize(Timer timer, Money money, List<Shelf> shelves)
    {
        _timer = timer;
        _money = money;
        _shelfs = shelves;
    }

    private void PauseTime()
    {
        if(_money.CyrrentMoney >= _stopTimer.Prise)
        {
            _stopTimer.PauseForSeconds();
            _money.RemoveMoney(_stopTimer.Prise);
        }
    }

    private void CollectPairs()
    {
        if(_money.CyrrentMoney >= _collectingPair.Prise)
        {
            _collectingPair.StartCollectPairs(_shelfs);
            _money.RemoveMoney(_collectingPair.Prise);
        }
    }

    private void Replace()
    {
        if(_money.CyrrentMoney >= _replaceObjects.Prise)
        {
            _replaceObjects.StartReplaceObjects(_shelfs);
            _money.RemoveMoney(_replaceObjects.Prise);
        }
    }
}