using System.Collections.Generic;
using System.Linq;
using _Project.Code.Gameplay.Shelfs;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.SceneArgs;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Entry.Bootstrappers
{
    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;
        
    }
    
    public class GameplayBoostersBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;
        [Inject] private ICoroutinePerformer _coroutinePerformer;
        [Inject] private IConfigProvider _configProvider;

        private VictoryLossService _victoryLossService;
        private ScorePresenter _walletPresenter;
        private MatchService _matchService;
        private Timer _timer;
        private TimerPresenter _timerPresenter;
        private BoostActivator _boostActivator;
        private List<Shelf> _shelves;

        private void Awake()
        {
            var message = _inputArgs.Input.Resolve<string>();
            Debug.Log(message);    
        }

        public override void InstallBindings()
        {
            var windowService = FindObjectOfType<WindowService>();
            var walletView = FindObjectOfType<ScoreView>();
            var shelfs = FindObjectsOfType<Shelf>();
            _shelves = shelfs.ToList();
            var timerView = FindObjectOfType<TimerView>();

            _timer = new Timer(_coroutinePerformer, 30);
            _timerPresenter = new TimerPresenter(_timer, timerView);
            _victoryLossService = new VictoryLossService(_timer, windowService);
            Score wallet = new Score();
            _walletPresenter = new ScorePresenter(wallet, walletView);
            _matchService = new MatchService(wallet, _shelves);
            _boostActivator = new BoostActivator(_configProvider, Container);

            Container.Bind<Timer>().FromInstance(_timer);
            Container.Bind<List<Shelf>>().FromInstance(_shelves);
            Container.Bind<BoostActivator>().FromInstance(_boostActivator);
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