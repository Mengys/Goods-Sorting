using _Project.Code.Data.Static.Sound;

namespace _Project.Code.Services.SoundPlayer
{
    public interface ISoundPlayer
    {
        void PlaySound(SoundId id);
    }
}