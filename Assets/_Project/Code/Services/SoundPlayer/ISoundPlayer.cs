using _Project.Code.Services.SoundPlayer.Config;

namespace _Project.Code.Services.SoundPlayer
{
    public interface ISoundPlayer
    {
        void PlaySound(SoundId id);
    }
}