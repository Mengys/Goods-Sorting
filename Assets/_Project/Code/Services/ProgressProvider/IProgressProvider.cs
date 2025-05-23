using _Project.Code.Data.Dynamic.PlayerProgress;

namespace _Project.Code.Services.ProgressProvider
{
    public interface IProgressProvider
    {
        ReactivePlayerProgress PlayerProgress { get; }
        void IncrementPassedLevel();
    }
}