using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Services.ParticlesPlayer.Config
{
    [CreateAssetMenu(fileName = "ParticlesConfig", menuName = "Configs/ParticlesConfig")]
    public class ParticlesConfig : ScriptableObject
    {
        [field: SerializeField] public List<ParticleConfig> ParticleConfigs;
    }
}