using R3;

namespace _Project.Code.Gameplay.Timer
{
    public interface ITimer
    {
        public ReadOnlyReactiveProperty<float> RemainingSeconds { get; }
        public Observable<Unit> Elapsed { get; }

        public void Setup(float seconds);
        public void Start();
        public void Stop();
    }
}