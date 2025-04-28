using System;
using System.Linq;
using _Project.Code.Architecture.Services.EffectsPlayer;
using UnityEngine;

namespace _Project.Code
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/ConfigProvider")]
    public class ConfigProvider : ScriptableObject
    {
        [field: SerializeField] public GameStatesConfig GameStates { get; private set; }

        [field: SerializeField]
        public ParticlesConfig Particles { get; private set; }

        public ParticleConfig GetConfigFor(ParticleId id)
        {
            var config = Particles.ParticleConfigs.FirstOrDefault(x => x.Id == id);

            if (config.Id == ParticleId.None)
                throw new ArgumentException($"Particle config for {id} is not defined");

            return config;
        }
    }
}