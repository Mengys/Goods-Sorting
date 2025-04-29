using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Code.Architecture.Services.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        public string CurrentSceneName => SceneManager.GetActiveScene().name;

        public IEnumerator LoadAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            ValidateScene(sceneName);

            AsyncOperation waitLoading = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            while (waitLoading?.isDone == false)
                yield return null;
        }

        public IEnumerator UnloadAsync(string sceneName)
        {
            ValidateScene(sceneName);

            AsyncOperation waitLoading = SceneManager.UnloadSceneAsync(sceneName);

            while (waitLoading?.isDone == false)
                yield return null;
        }
        
        private void ValidateScene(string sceneName)
        {
            if (!SceneExists(sceneName))
                throw new System.Exception($"Scene {sceneName} not found");
        }

        private bool SceneExists(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                string nameFromPath = System.IO.Path.GetFileNameWithoutExtension(path);
              
                if (nameFromPath == sceneName)
                    return true;
            }
            
            return false;
        }
    }
}