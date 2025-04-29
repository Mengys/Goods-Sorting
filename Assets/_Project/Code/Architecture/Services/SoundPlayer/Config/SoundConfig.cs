using UnityEngine;

namespace _Project.Code.Architecture.Services.SoundPlayer
{
    [System.Serializable]
    public struct SoundConfig
    {
        public SoundId Id;
        public AudioClip AudioClip;
    }
}