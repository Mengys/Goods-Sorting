using UnityEngine;

namespace _Project.Code.Architecture.Services.SoundPlayer
{
    public class SoundPlayer : ISoundPlayer
    {
        private readonly ConfigProvider _configProvider;

        public SoundPlayer(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void PlaySound(SoundId id)
        {
            SoundConfig config = _configProvider.GetConfigFor(id);
            
            if (config.AudioClip != null)
            {
                AudioSource.PlayClipAtPoint(config.AudioClip, Vector3.zero);
            }
        }
    }
}