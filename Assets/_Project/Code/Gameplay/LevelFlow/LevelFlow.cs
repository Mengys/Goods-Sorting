using System;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Infrastructure.Bootstrappers;
using _Project.Code.Services.BoosterUser;
using _Project.Code.Services.PauseHandler;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.UI.Buttons.Window;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.LevelFlow
{
    public class LevelFlow : IPausable, ILevelFlow, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        private readonly ITimer _timer;
        private readonly IGrid _grid;
        private readonly IPauseHandler _pauseHandler;
        private readonly GameOverHandler _gameOverHandler;

        private readonly IItemCollectHandler _itemCollectHandler;
        private readonly IComboHandler _comboHandler;
        private readonly IBoosterUser _boosterUser;
        private readonly IProgressProvider _progressProvider;
        
        private BoosterId? _initialBoosterId;
            
        public LevelFlow(
            IGrid grid,
            ITimer timer,
            IPauseHandler pauseHandler,
            GameOverHandler gameOverHandler,
            IItemCollectHandler itemCollectHandler,
            IComboHandler comboHandler,
            IBoosterUser boosterUser,
            IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
            _comboHandler = comboHandler;
            _boosterUser = boosterUser;
            _itemCollectHandler = itemCollectHandler;
            _gameOverHandler = gameOverHandler;
            _pauseHandler = pauseHandler;
            _grid = grid;
            _timer = timer;
        }

        private Observable<Unit> TimerElapsed => _timer.Elapsed;
        private Observable<Unit> MatchCollected => _grid.MatchCollected.AsUnitObservable();
        private Observable<Unit> FirstMoveMade => _grid.FirstMoveMade;
        private Observable<Unit> AllMatchesCollected => _grid.AllMatchesCollected;
        private Observable<Unit> FirstLayerFilled => _grid.FirstLayerFilled;

        private int LevelId => _progressProvider.PlayerProgress.Level.Id;

        public void Initialize(BoosterId? initialBoosterId)
        {
            _initialBoosterId = initialBoosterId;

            _pauseHandler.Register(this).AddTo(_disposable);

            if (_comboHandler is IPausable pausable)
                _pauseHandler.Register(pausable);

            FirstMoveMade.Subscribe(_ => Start());
            
            TimerElapsed.Subscribe(_ =>
            {
                _gameOverHandler.HandleTimeLose();
                HandleLevelLoseAnalytics();
            }).AddTo(_disposable);
            
            FirstLayerFilled.Subscribe(_ =>
            {
                _gameOverHandler.HandleOutOfFreeCellsLose();
                HandleLevelLoseAnalytics();
            }).AddTo(_disposable);
            
            AllMatchesCollected.Subscribe(_ =>
            {
                _gameOverHandler.HandleWin();
                HandleLevelWinAnalytics();
            }).AddTo(_disposable);
            
            MatchCollected.Subscribe(_ => _itemCollectHandler.Handle(3)).AddTo(_disposable);

            _comboHandler.Initialize(_itemCollectHandler);

            _grid.Initialize();
            _timer.Setup(60);
            
            if (_initialBoosterId.HasValue)
            {
                Debug.Log($"Using initial booster {_initialBoosterId.Value}");
                _boosterUser.Use(_initialBoosterId.Value);
            }
        }

        private void HandleLevelLoseAnalytics()
        {
            Firebase.Analytics.FirebaseAnalytics
                .LogEvent(
                    "level_lose",
                    Firebase.Analytics.FirebaseAnalytics.ParameterLevelName,
                    LevelId);
        }
        
        private void HandleLevelWinAnalytics()
        {
            Firebase.Analytics.FirebaseAnalytics
                .LogEvent(
                    "level_win",
                    Firebase.Analytics.FirebaseAnalytics.ParameterLevelName,
                    LevelId);
        }

        private void HandleLevelStartAnalytics()
        {
            Firebase.Analytics.FirebaseAnalytics
                .LogEvent(
                    Firebase.Analytics.FirebaseAnalytics.EventLevelStart,
                    Firebase.Analytics.FirebaseAnalytics.ParameterLevelName,
                    LevelId);    
        }
        
        private void Start()
        {
            _timer.Start();
            HandleLevelStartAnalytics();
        }

        public void Pause()
        {
            _timer.Stop();
            _grid.Disable();
        }

        public void Resume()
        {
            _timer.Start();
            _grid.Enable();
        }

        public void ContinueWithAdditionalTime(float seconds)
        {
            _timer.Setup(seconds);
            _timer.Start();

            _pauseHandler.Resume();
        }

        public void Dispose() =>
            _disposable?.Dispose();
    }
}