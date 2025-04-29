using System;
using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.State;
using R3;

namespace _Project.Code.UI.Window.Implementations.LevelInfo
{
    public class LevelInfoWindowPresenter : IDisposable, IWindow
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly LevelInfoWindow _view;
        
        public LevelInfoWindowPresenter(
            LevelInfoWindowModel model,
            LevelInfoWindow view,
            IStateMachine<GameStateId> stateMachine)
        {
            _view = view;
            
            view.PlayClicked
                .Subscribe(_ => stateMachine.Enter(GameStateId.Gameplay))
                .AddTo(_disposables);
            
            view.Initialize(model.Level, model.Difficulty);
        }

        public void Dispose() => _disposables.Dispose();
        
        public void Open() => _view.Open();

        public void Close() => _view.Close();
    }
}