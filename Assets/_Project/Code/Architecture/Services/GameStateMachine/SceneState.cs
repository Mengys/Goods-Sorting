using System.Collections;
using _Project.Code.Architecture;
using UnityEngine;
using Zenject;

namespace _Project.Code
{
    public class SceneState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutinePerformer _coroutinePerformer;

        private readonly SceneArgs _args;
        private readonly string _sceneName;

        public SceneState(
            ICoroutinePerformer coroutinePerformer, 
            ISceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            string sceneName, SceneArgs args)
        {
            
            _coroutinePerformer = coroutinePerformer;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            
            _sceneName = sceneName;
            _args = args;
        }

        private bool IsLoaded(string sceneName) =>
            _sceneLoader.CurrentSceneName == sceneName;

        public void Enter()
        {
            _coroutinePerformer.Start(EnterRoutine());
        }

        public void Exit()
        {
        }

        private IEnumerator EnterRoutine()
        {
            yield return _loadingCurtain.Show();

            if (!IsLoaded(_sceneName))
                yield return _sceneLoader.LoadAsync(_sceneName);

            InitializeScene(_args);

            yield return _loadingCurtain.Hide();
        }

        private void InitializeScene(SceneArgs args)
        {
            var sceneContext = Object.FindObjectOfType<SceneContext>();
            
            sceneContext.Container.Bind<SceneArgs>().FromInstance(args).AsSingle().NonLazy();
        }
    }
}