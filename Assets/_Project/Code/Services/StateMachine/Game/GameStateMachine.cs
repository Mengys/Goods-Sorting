using System;
using System.Collections.Generic;
using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.Curtain;
using Zenject;

namespace _Project.Code.Services.StateMachine.Game
{
    public class GameStateMachine : IStateMachine<GameStateId>
    {
        private GameStateId _stateId;
        
        private readonly Dictionary<GameStateId, IState> _states;
        private readonly SceneArgs.SceneArgs _args;
        private LoadingCurtain _loadingCurtain;

        public GameStateMachine(IFactory<GameStateId, IState> factory, LoadingCurtain loadingCurtain,  SceneArgs.SceneArgs args)
        {
            _loadingCurtain = loadingCurtain;
            _args = args;

            _states = new Dictionary<GameStateId, IState>
            {
                { GameStateId.Entry, factory.Create(GameStateId.Entry) },
                { GameStateId.Gameplay, factory.Create(GameStateId.Gameplay) },
                { GameStateId.Menu, factory.Create(GameStateId.Menu) },
            };
        }

        public void Enter(GameStateId stateId) =>
            HandleTransition(_stateId, stateId);

        private void ExitCurrentState()
        {
            _states[_stateId].Exit();
            _stateId = GameStateId.None;
        }

        private void HandleTransition(GameStateId from, GameStateId to)
        {
            if (to is GameStateId.None)
                throw new ArgumentException("Cannot enter None state");

            if (to == from && from != GameStateId.Gameplay) 
                return;

            if (from is not GameStateId.None)
                ExitCurrentState();

            _args.Input = _args.Output;
            _args.Output = new DiContainer();
            
            _loadingCurtain.Show();
            
            _stateId = to;
            _states[_stateId].Enter();
        }
    }
}