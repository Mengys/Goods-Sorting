using _Project.Code.Data.Static.Particles;
using UnityEngine;

namespace _Project.Code.Services.ParticlesPlayer
{
    public interface IParticlesPlayer
    {
        void Play(ParticleId id, Vector3 position);
    }
}