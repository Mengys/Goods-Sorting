using _Project.Code.Services.ConfigProvider;
using ParticlesPlayer;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.ParticlesPlayer
{
    public class ParticlesPlayer : IParticlesPlayer
    {
        private readonly IConfigProvider _configProvider;

        [Inject]
        public ParticlesPlayer(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void Play(ParticleId id, Vector3 position)
        {
            var config = _configProvider.ForParticle(id).Value;

            var instance =
                Object.Instantiate(config.ParticleSystem, position, Quaternion.identity);

            instance.Play();
        }
    }
}