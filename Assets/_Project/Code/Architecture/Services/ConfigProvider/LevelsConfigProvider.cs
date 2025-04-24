using UnityEngine;

namespace _Project.Code
{
    [CreateAssetMenu(fileName = "LevelsConfigProvider", menuName = "Configs/LevelsConfigProvider")]
    public class LevelsConfigProvider : ScriptableObject
    {
        [field: SerializeField] public LevelConfig[] Configs { get; private set; }
    }
}