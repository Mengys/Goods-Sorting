using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.Counter
{
    public struct Coins
    {
    }

    public struct Score
    {
    }

    public struct Hearts
    {
    }
    
    public class Counter<T> : ICounter<T>
    {
        private readonly ReactiveProperty<int> _value = new(0);

        public ReadOnlyReactiveProperty<int> Reactive => _value;

        public int Value => _value.Value;
        
        public void Add(int score) => 
            _value.Value += Mathf.Max(0, score);
        
        public void Remove(int score) => 
            _value.Value = Mathf.Max(0, _value.Value - score);
    }
}