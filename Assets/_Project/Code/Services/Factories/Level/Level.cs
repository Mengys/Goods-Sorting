using _Project.Code.Gameplay.Grid;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Services.Factories.UI.WindowFactory;
using _Project.Code.UI.Windows;
using R3;
using UnityEngine;

namespace _Project.Code.Services.Factories.Level
{
    public class Level
    {
        private readonly CompositeDisposable _disposable = new();
        
        private readonly GridPresenter _grid;
        private readonly IWindowFactory _windowFactory;
        private Timer _timer;

        public Level(GridPresenter grid, IWindowFactory windowFactory, Timer timer)
        {
            _timer = timer;
            _windowFactory = windowFactory;
            _grid = grid.AddTo(_disposable);
        }
        
        public void Initialize(Observable<Unit> won, Observable<Unit> lost)
        {
            lost.Subscribe(_ => OnLose()).AddTo(_disposable);
            won.Subscribe(_ => OnWin()).AddTo(_disposable);
        }

        public Observable<int> MatchHandled => _grid.MatchHandled;
        public Observable<Unit> FirstMoveMade => _grid.FirstMoveMade;
        public Observable<Unit> AllMatchesCollected => _grid.AllMatchesCollected.Take(1);

        public void Dispose() => _disposable.Dispose();

        public void ContinueWithAdditionalTime(float seconds)
        {
            _timer.Stop();
            _timer.Initialize(seconds);
            _timer.Start();
         
            _grid.Continue();
            
            Debug.Log("Continue with additional time: " + seconds);
        }

        private void OnLose()
        {
            _grid.Pause();
            _windowFactory.Create(WindowId.Lose);
        }

        private void OnWin()
        {
            _grid.Pause();
            _windowFactory.Create(WindowId.Win);
        }
    }
}