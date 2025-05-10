using System;
using _Project.Code.Gameplay.Levels.Configs;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.Factories.Level;
using _Project.Code.Services.SceneArgs;
using _Project.Code.UI.Windows.Implementations.LevelInfo;
using R3;
using Zenject;

namespace _Project.Code.Gameplay.Wallet.Infrastructure.Bootstrappers
{
    [Serializable]
    public class PlayerProgress
    {
        public int Coins;
        public LevelInfo Level = LevelInfo.Default;
    }

    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;
        [Inject] private ICoroutinePerformer _coroutinePerformer;
        [Inject] private IConfigProvider _configProvider;

        private void Awake() => LoadLevel();

        private void Start() => ApplyInitialBooster();

        private void LoadLevel()
        {
            var timer = Container.Resolve<Timer.Timer>();
            timer.Initialize(60);

            var playerProgress = Container.Resolve<PlayerProgress>();

            var levelGenerator = Container.Resolve<ILevelFactory>();

            var level = levelGenerator.Generate(playerProgress.Level.Id);

            Container.Bind<Level>().FromInstance(level);

            var score = Container.Resolve<Score.Score>();

            level.MatchHandled.Subscribe(count => score.AddScore(count));

            level.FirstMoveMade.Subscribe(_ => { StartLevel(level); });

            Container.Resolve<LoadingCurtain>().Hide();
        }

        private void StartLevel(Level level)
        {
            var timer = Container.Resolve<Timer.Timer>();

            var lost = timer.Elapsed;
            var won = level.AllMatchesCollected;

            won.Subscribe(_ =>
            {
                var configProvider = Container.Resolve<IConfigProvider>();
                var playerProgress = Container.Resolve<PlayerProgress>();

                var nextId = playerProgress.Level.Id + 1;

                LevelConfig? nextLevel = configProvider.ForLevel(nextId);

                if (nextLevel != null)
                {
                    playerProgress.Level = new LevelInfo
                    {
                        Number = nextId + 1,
                        Difficulty = nextLevel.Value.Difficulty
                    };
                }
                
                timer.Stop();
            });

            level.Initialize(won, lost);

            timer.Start();
        }

        private void ApplyInitialBooster()
        {
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Score.Score>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<Timer.Timer>().AsSingle();
        }
    }
}