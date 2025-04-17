using System;
using System.Collections.Generic;
using _Project.Code.Architecture;
using Zenject;

namespace _Project.Code
{
    public class GameStateMachine : IStateMachine<GameState>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly SceneArgs _args;

        private readonly Dictionary<GameState, IState> _states;

        private GameState _state;

        [Inject]
        public GameStateMachine(
            ISceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            ICoroutinePerformer coroutinePerformer,
            SceneArgs args)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _coroutinePerformer = coroutinePerformer;
            _args = args;

            _states = new Dictionary<GameState, IState>
            {
                { GameState.Entry, GetNewStateFor(GameState.Entry) },
                { GameState.Gameplay, GetNewStateFor(GameState.Gameplay) },
                { GameState.Menu, GetNewStateFor(GameState.Menu) },
            };
        }

        public void Enter(GameState state) =>
            HandleTransition(_state, state);

        private void ExitCurrentState()
        {
            _states[_state].Exit();
            _state = GameState.None;
        }

        private void HandleTransition(GameState from, GameState to)
        {
            if (to is GameState.None)
                throw new ArgumentException("Cannot enter None state");

            if (to == from) return;

            if (from is not GameState.None) ExitCurrentState();

            _args.Input = _args.Output;
            _args.Output = new DiContainer();

            _state = to;
            _states[_state].Enter();
        }

        private IState GetNewStateFor(GameState state) =>
            new SceneState(_coroutinePerformer, _sceneLoader, _loadingCurtain, state.ToString());
    }
}