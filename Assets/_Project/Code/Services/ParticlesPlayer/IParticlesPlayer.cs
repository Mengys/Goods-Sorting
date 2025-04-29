using UnityEngine;

namespace ParticlesPlayer
{
    public interface IParticlesPlayer
    {
        void Play(ParticleId id, Vector3 position);
    }
}