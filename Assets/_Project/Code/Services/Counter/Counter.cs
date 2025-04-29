using R3;

namespace _Project.Code.Services.Counter
{
    public class Counter : ICounter
    {
        private readonly ReactiveProperty<int> _value = new(0);

        public Observable<int> Value => _value;

        public void Increment(int count = 1) => _value.Value++;

        public void Decrement(int count = 1) => _value.Value--;
    }

    public class MoneyCounter : Counter
    {
    }

    public class HealthCounter : Counter
    {
    }

    public class ExperienceCounter : Counter
    {
    }

    public interface ICounter
    {
        Observable<int> Value { get; }
        void Increment(int count = 1);
        void Decrement(int count = 1);
    }
}