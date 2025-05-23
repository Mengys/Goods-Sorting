using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.SceneLoader;
using _Project.Code.Services.StateMachine;
using _Project.Code.Services.StateMachine.Game;
using Zenject;

namespace _Project.Code.Services.Factories.GameStates
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
            var config = _configProvider.ForState(id).Value;
            
            return new GameState(_coroutinePerformer, _sceneLoader, config.SceneName);
        }
    }
}