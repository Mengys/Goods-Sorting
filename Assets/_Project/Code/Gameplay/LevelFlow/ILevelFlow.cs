namespace _Project.Code.Gameplay.LevelFlow
{
    public interface ILevelFlow
    {
        void Initialize();
        void Start();
        void ContinueWithAdditionalTime(float seconds);
    }
}