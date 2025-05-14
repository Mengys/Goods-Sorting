using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs
{
    [CreateAssetMenu(fileName = nameof(BoosterConfigList), menuName = "Configs/Lists/" + nameof(BoosterConfigList))]
    public class BoosterConfigList : ScriptableObject
    {
        [field: SerializeField] public List<BoosterConfig> Configs { get; private set; }
    }
}