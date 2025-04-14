using System.Collections;
using _Project.Code.Architecture;
using Zenject;

namespace _Project.Code
{
    public class GameStateMachine : IStateMachine<GameState>
    {
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private LoadingCurtain _loadingCurtain;
        [Inject] private ICoroutinePerformer _coroutinePerformer;

        public void Enter(GameState state)
        {
            var sceneName = state.ToString();

            if (_sceneLoader.CurrentSceneName != sceneName) 
                _coroutinePerformer.Start(Load(sceneName));
        }
        
        private IEnumerator Load(string sceneName)
        {
            yield return _loadingCurtain.Show();
            yield return _sceneLoader.LoadAsync(sceneName);
            yield return _loadingCurtain.Hide();
        }
    }
}