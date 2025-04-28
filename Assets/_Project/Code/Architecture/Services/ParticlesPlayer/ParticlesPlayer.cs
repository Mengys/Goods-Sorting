using _Project.Code.Architecture.Services.EffectsPlayer;
using UnityEngine;
using Zenject;

namespace _Project.Code.Architecture.Services.ParticlesPlayer
{
    public class ParticlesPlayer : IParticlesPlayer
    {
        private readonly ConfigProvider _configProvider;

        [Inject]
        public ParticlesPlayer(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void Play(ParticleId id, Vector3 position)
        {
            var config = _configProvider.GetConfigFor(id);

            var instance =
                Object.Instantiate(config.ParticleSystem, position, Quaternion.identity);

            instance.Play();
        }
    }
}