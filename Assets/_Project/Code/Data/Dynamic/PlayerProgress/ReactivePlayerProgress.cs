using System;
using R3;

namespace _Project.Code.Data.Dynamic.PlayerProgress
{
    public class ReactivePlayerProgress : IDisposable
    {
        private PlayerProgress _data;
        private readonly CompositeDisposable _disposables = new();

        public ReactivePlayerProgress()
        {
            Value = new PlayerProgress();
        }
        
        public ReactiveProperty<int> CoinsReactive {get; private set;}
        public ReactiveProperty<LevelInfo> LevelReactive {get; private set;}

        public PlayerProgress Value
        {
            set
            {
                _data = value;
                
                CoinsReactive = new ReactiveProperty<int>(_data.Coins).AddTo(_disposables);
                LevelReactive = new ReactiveProperty<LevelInfo>(_data.Level).AddTo(_disposables);
                
                CoinsReactive
                    .Subscribe(newValue => _data.Coins = newValue)
                    .AddTo(_disposables);

                LevelReactive
                    .Subscribe(newValue => _data.Level = newValue)
                    .AddTo(_disposables);
            }
        }

        public PlayerProgress Serializable => _data;

        public LevelInfo Level
        {
            get => LevelReactive.Value;
            set => LevelReactive.Value = value;
        }

        public int Coins
        {
            get => CoinsReactive.Value;
            set => CoinsReactive.Value = value;
        }

        public void Dispose() => _disposables.Dispose();
    }
}