using System.Collections;
using UnityEngine.SceneManagement;

namespace _Project.Code.Services.SceneLoader
{
    public interface ISceneLoader
    {
        string CurrentSceneName { get; }
        IEnumerator LoadAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
        IEnumerator UnloadAsync(string sceneName);
    }
}