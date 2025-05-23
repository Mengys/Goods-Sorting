using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Data.Static.Level
{
    [CreateAssetMenu(fileName = "LevelConfigList", menuName = "Configs/Lists/LevelConfigList")]
    public class LevelConfigList : ScriptableObject
    {
        [field: SerializeField] public List<LevelConfigAsset> Configs { get; private set; }
    }
}