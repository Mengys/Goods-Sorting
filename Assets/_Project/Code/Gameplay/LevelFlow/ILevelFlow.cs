using _Project.Code.Data.Static.Booster;

namespace _Project.Code.Gameplay.LevelFlow
{
    public interface ILevelFlow
    {
        void Initialize(BoosterId? initialBoosterId);
        void ContinueWithAdditionalTime(float seconds);
    }
}