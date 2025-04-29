using System;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.SceneLoading;
using Zenject;

namespace _Project.Code.Infrastructure.GameStateMachine.Factory
{
    public class GameStatesFactory : IFactory<GameStateId, IState>
    {
        private readonly IConfigProvider _configProvider;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly LoadingCurtain _loadingCurtain;
        
        public GameStatesFactory(DiContainer container)
        {
            _configProvider = container.Resolve<IConfigProvider>();
            _sceneLoader = container.Resolve<ISceneLoader>();
            _coroutinePerformer = container.Resolve<ICoroutinePerformer>();
            _loadingCurtain = container.Resolve<LoadingCurtain>();
        }
        
        public IState Create(GameStateId id)
        {
            var config = _configProvider.ForState(id);
            
            if (config == null)
                throw new NullReferenceException($"Config for state {id} is not found");
                
            return new GameState(_coroutinePerformer, _sceneLoader, _loadingCurtain, config.Value.SceneName);
        }
    }
}