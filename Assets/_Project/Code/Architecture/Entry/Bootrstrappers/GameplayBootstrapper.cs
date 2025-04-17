using _Project.Code.Architecture;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Project.Code
{
    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;
        [Inject] private ICoroutinePerformer _coroutinePerformer;

        private VictoryLossService _victoryLossService;
        private WalletPresenter _walletPresenter;
        private MatchService _matchService;
        private Timer _timer;
        private TimerPresenter _timerPresenter;

        private void Awake()
        {
            var message = _inputArgs.Input.Resolve<string>();
            Debug.Log(message);

            var windowService = FindObjectOfType<WindowService>();   
            var walletView = FindObjectOfType<WalletView>();
            var shelfs = FindObjectsOfType<Shelf>();
            var timerView = FindObjectOfType<TimerView>();

            _timer = new Timer(_coroutinePerformer, 30);
            _timerPresenter = new TimerPresenter(_timer, timerView);
            _victoryLossService = new VictoryLossService(_timer, windowService);

            Wallet wallet = new Wallet();
            _walletPresenter = new WalletPresenter(wallet, walletView);
            _matchService = new MatchService(wallet, shelfs.ToList());
        }

        public override void InstallBindings()
        {

        }

        private void OnDisable()
        {
            _walletPresenter.Dispose();
            _matchService.Dispose();
            _timerPresenter.Dispose();
        }

        private void Start()
        {

            _timer.StartTimer();
        }

        private void Update()
        {
            _victoryLossService.Update();
        }
    }
}