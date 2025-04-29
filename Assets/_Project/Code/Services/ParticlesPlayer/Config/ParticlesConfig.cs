using System.Collections.Generic;
using UnityEngine;

namespace ParticlesPlayer
{
    [CreateAssetMenu(fileName = "ParticlesConfig", menuName = "Configs/ParticlesConfig")]
    public class ParticlesConfig : ScriptableObject
    {
        [field: SerializeField] public List<ParticleConfig> ParticleConfigs;
    }
}