using System;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.SceneArgs;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public struct BoosterId
    {
    }

    public struct LevelStartData
    {
        public BoosterId? InitialBoosterId;
    }

    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;
        [Inject] private ICoroutinePerformer _coroutinePerformer;
        [Inject] private IConfigProvider _configProvider;
        
        private LevelStartData _levelStartData;

        public override void InstallBindings()
        {
            /*
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
            */
        }

        private void Awake()
        {
            if (!_inputArgs.Input.HasBinding<LevelStartData>())
                throw new ArgumentNullException();

            _levelStartData = _inputArgs.Input.Resolve<LevelStartData>();
        }

        private void Start()
        {
            LoadLevel();
            ApplyInitialBooster();
        }
        
        private void LoadLevel()
        {
        }

        private void ApplyInitialBooster()
        {
        }
    }
}