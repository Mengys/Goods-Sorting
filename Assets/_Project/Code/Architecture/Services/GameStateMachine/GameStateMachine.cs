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

        private readonly Dictionary<GameState, SceneArgs> _args;
        private readonly Dictionary<GameState, IState> _states;

        private GameState _state;

        [Inject]
        public GameStateMachine(ISceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            ICoroutinePerformer coroutinePerformer)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _coroutinePerformer = coroutinePerformer;

            _args = new Dictionary<GameState, SceneArgs>
            {
                { GameState.Entry, new SceneArgs() },
                { GameState.Gameplay, new SceneArgs() },
                { GameState.Menu, new SceneArgs() },
            };

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
            if (to == GameState.None)
                throw new ArgumentException("Cannot enter None state");

            if (from == to) return;

            if (from != GameState.None)
            {
                var output = _args[from].Output;

                ExitCurrentState();

                _args[to].Input = output;
            }
            else
            {
                _args[to].Input = new DiContainer();
            }

            _args[to].Output = new DiContainer();

            _state = to;
            _states[_state].Enter();
        }

        private IState GetNewStateFor(GameState state) =>
            new SceneState(_coroutinePerformer, _sceneLoader, _loadingCurtain, state.ToString(), _args[state]);
    }
}