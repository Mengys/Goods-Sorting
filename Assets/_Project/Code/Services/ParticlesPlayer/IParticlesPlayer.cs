using _Project.Code.Services.ParticlesPlayer.Config;
using UnityEngine;

namespace _Project.Code.Services.ParticlesPlayer
{
    public interface IParticlesPlayer
    {
        void Play(ParticleId id, Vector3 position);
    }
}