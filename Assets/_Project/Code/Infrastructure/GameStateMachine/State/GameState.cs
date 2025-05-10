using System.Collections;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.Curtain;
using _Project.Code.Services.SceneLoading;

namespace _Project.Code.Infrastructure.GameStateMachine.State
{
    public class GameState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutinePerformer _coroutinePerformer;

        private readonly string _sceneName;

        public GameState(
            ICoroutinePerformer coroutinePerformer, 
            ISceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            string sceneName)
        {
            
            _coroutinePerformer = coroutinePerformer;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _sceneName = sceneName;
        }

        private bool IsLoaded(string sceneName) =>
            _sceneLoader.CurrentSceneName == sceneName;

        public void Enter() => 
            _coroutinePerformer.Start(LoadScene());

        public void Exit()
        {
        }

        private IEnumerator LoadScene()
        {
            if (!IsLoaded(_sceneName))
                yield return _sceneLoader.LoadAsync(_sceneName);
        }
    }
}