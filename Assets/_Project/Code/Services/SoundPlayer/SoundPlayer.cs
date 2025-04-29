using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.SoundPlayer.Config;
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