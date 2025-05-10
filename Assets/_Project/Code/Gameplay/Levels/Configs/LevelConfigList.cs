using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Levels.Configs
{
    [CreateAssetMenu(fileName = "LevelConfigList", menuName = "Configs/LevelConfigList")]
    public class LevelConfigList : ScriptableObject
    {
        [field: SerializeField] public List<LevelConfigAsset> Configs { get; private set; }
    }
}