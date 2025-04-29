using System.Collections.Generic;
using _Project.Code.Moneys;
using _Project.Code.Shelfs;
using _Project.Code.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Boosters
{
    public class BostersController : MonoBehaviour
    {
        [SerializeField] private Button _stopTimerButton;
        [SerializeField] private Button _collectingPairButton;
        [SerializeField] private Button _replaceObjectsButton;
        [SerializeField] private Button _shuffleButton;
        [SerializeField] private Button _bombButton;

        [SerializeField] private StopTimer _stopTimer;
        [SerializeField] private CollectingPair _collectingPair;
        [SerializeField] private ReplaceObjects _replaceObjects;
        [SerializeField] private Shuffle _shuffle;
        [SerializeField] private Bomb _bomb;

        private Timer _timer;
        private Money _money;
        private List<Shelf> _shelfs;

        private void OnEnable()
        {
            _stopTimerButton.onClick.AddListener(PauseTime);
            _collectingPairButton.onClick.AddListener(CollectPairs);
            _replaceObjectsButton.onClick.AddListener(Replace);
            _shuffleButton.onClick.AddListener(Shuffle);
            _bombButton.onClick.AddListener(BombActivate);
        }

        private void OnDisable()
        {
            _stopTimerButton.onClick.RemoveListener(PauseTime);
            _collectingPairButton.onClick.RemoveListener(CollectPairs);
            _replaceObjectsButton.onClick.RemoveListener(Replace);
            _shuffleButton.onClick.RemoveListener(Shuffle);
            _bombButton.onClick.RemoveListener(BombActivate);
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
            if (_money.CyrrentMoney >= _stopTimer.Prise)
            {
                _stopTimer.PauseForSeconds();
                _money.RemoveMoney(_stopTimer.Prise);
            }
        }

        private void CollectPairs()
        {
            if (_money.CyrrentMoney >= _collectingPair.Prise)
            {
                _collectingPair.StartCollectPairs(_shelfs);

                if (_collectingPair.IsEnough)
                {
                    _money.RemoveMoney(_collectingPair.Prise);
                }
            }
        }

        private void Replace()
        {
            if (_money.CyrrentMoney >= _replaceObjects.Prise)
            {
                _replaceObjects.StartReplaceObjects(_shelfs);

                if (_replaceObjects.IsEnough)
                {
                    _money.RemoveMoney(_replaceObjects.Prise);
                }
            }
        }

        private void Shuffle()
        {
            if (_money.CyrrentMoney >= _shuffle.Prise)
            {
                _shuffle.StartShuffle(_shelfs);
                _money.RemoveMoney(_shuffle.Prise);
            }
        }

        private void BombActivate()
        {
            if (_money.CyrrentMoney >= _bomb.Prise)
            {
                _bomb.Activate(_shelfs);
                _money.RemoveMoney(_bomb.Prise);
            }
        }
    }
}