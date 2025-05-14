using _Project.Code.Data.Static.Sound;
using _Project.Code.Services.ConfigProvider;
using UnityEngine;

namespace _Project.Code.Services.SoundPlayer
{
    public class SoundPlayer : ISoundPlayer
    {
        private readonly IConfigProvider _configProvider;

        public SoundPlayer(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void PlaySound(SoundId id)
        {
            SoundConfig config = _configProvider.ForSound(id).Value;

            AudioSource.PlayClipAtPoint(config.AudioClip, Vector3.zero);
        }
    }
}