using System.Collections;
using _Project.Code.Architecture;

namespace _Project.Code
{
    public class SceneState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutinePerformer _coroutinePerformer;

        private readonly string _sceneName;

        public SceneState(
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
            yield return _loadingCurtain.Show();

            if (!IsLoaded(_sceneName))
                yield return _sceneLoader.LoadAsync(_sceneName);

            yield return _loadingCurtain.Hide();
        }
    }
}