using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Data.Static.Sound
{
    [CreateAssetMenu(fileName = "SoundListConfig", menuName = "Configs/Lists/SoundListConfig", order = 0)]
    public class SoundListConfig : ScriptableObject
    {
        [field: SerializeField] public List<SoundConfig> Sounds { get; set; }
    }
}