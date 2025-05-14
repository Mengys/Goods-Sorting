using R3;

namespace _Project.Code.Gameplay.Counter
{
    public interface ICounter<T>
    {
        int Value { get; }
        ReadOnlyReactiveProperty<int> Reactive { get; }
        
        void Add(int score);
        void Remove(int score);
    }
}