using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Architecture.Services.SoundPlayer
{
    [CreateAssetMenu(fileName = "SoundsConfig", menuName = "Sounds/SoundsConfig", order = 0)]
    public class SoundsConfig : ScriptableObject
    {
        [field: SerializeField] public List<SoundConfig> Sounds { get; set; }
    }
}