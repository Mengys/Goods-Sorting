using UnityEngine;

namespace _Project.Code
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/ConfigProvider")]
    public class ConfigProvider : ScriptableObject
    {
        [field: SerializeField] public GameStatesConfig GameStates { get; private set; }
        [field: SerializeField] public LevelsConfigProvider LevelsConfigProvider { get; private set; }
    }
}