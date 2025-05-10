using System;

namespace _Project.Code.Gameplay.Score
{
    public class Score
    {
        private int _value = 0;

        public event Action<int> Changed;

        public int Value => _value;

        public void AddScore(int score)
        {
            _value += score;
            Changed?.Invoke(_value);
        }

        public void RemoveScore(int score)
        {
            _value -= score;
            Changed?.Invoke(_value);
        }
    }
}
