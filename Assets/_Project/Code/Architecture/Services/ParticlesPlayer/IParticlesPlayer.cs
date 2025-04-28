using UnityEngine;

namespace _Project.Code.Architecture.Services.EffectsPlayer
{
    public interface IParticlesPlayer
    {
        void Play(ParticleId id, Vector3 position);
    }
}