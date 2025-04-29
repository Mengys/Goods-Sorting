using System;
using System.Collections.Generic;
using _Project.Code.Infrastructure.Services.CoroutinePerformer;
using _Project.Code.Infrastructure.Services.Curtain;
using _Project.Code.Infrastructure.Services.GameStateMachine.Config;
using _Project.Code.Infrastructure.Services.GameStateMachine.State;
using _Project.Code.Infrastructure.Services.SceneLoading;
using Zenject;

namespace _Project.Code.Infrastructure.Services.GameStateMachine
{
    public class GameStateMachine : IStateMachine<GameStateId>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly SceneArgs.SceneArgs _args;

        private readonly Dictionary<GameStateId, IState> _states;

        private readonly GameStatesConfig _config;
        
        private GameStateId _stateId;

        [Inject]
        public GameStateMachine(
            ISceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            ICoroutinePerformer coroutinePerformer,
            ConfigProvider.ConfigProvider config,
            SceneArgs.SceneArgs args)
        {
            _config = config.GameStates;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _coroutinePerformer = coroutinePerformer;
            _args = args;

            _states = new Dictionary<GameStateId, IState>
            {
                { GameStateId.Entry, GetNewStateFor(GameStateId.Entry) },
                { GameStateId.Gameplay, GetNewStateFor(GameStateId.Gameplay) },
                { GameStateId.Menu, GetNewStateFor(GameStateId.Menu) },
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

            if (to == from) return;

            if (from is not GameStateId.None) ExitCurrentState();

            _args.Input = _args.Output;
            _args.Output = new DiContainer();

            _stateId = to;
            _states[_stateId].Enter();
        }

        private IState GetNewStateFor(GameStateId stateId)
        {
            if (!_config.StateScenes.TryGetValue(stateId, out var scene))
                throw new ArgumentException($"State {stateId} is not defined in config");

            if (string.IsNullOrEmpty(scene))
                throw new ArgumentException($"Scene for state {stateId} is not defined in config");

            return new SceneState(_coroutinePerformer, _sceneLoader, _loadingCurtain, scene);
        }
    }
}