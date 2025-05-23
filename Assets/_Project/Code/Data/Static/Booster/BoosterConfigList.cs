using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Data.Static.Booster
{
    [CreateAssetMenu(fileName = nameof(BoosterConfigList), menuName = "Configs/Lists/" + nameof(BoosterConfigList))]
    public class BoosterConfigList : ScriptableObject
    {
        [field: SerializeField] public List<BoosterConfig> Configs { get; private set; }
    }
}