using System;
using System.Linq;
using _Project.Code.Data.Dynamic.PlayerProgress.SerializableKeyValue;
using _Project.Code.UI.Elements.Booster.Inventory;
using _Project.Code.UI.Windows.Implementations;
using R3;
using UnityEngine;

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

        public ReactiveProperty<int> CoinsReactive { get; private set; }
        public ReactiveProperty<LevelInfo> LevelReactive { get; private set; }

        public BoosterInventoryData MenuBoosterInventory { get; private set; }
        public BoosterInventoryData GameplayBoosterInventory { get; private set; }

        public PlayerProgress Value
        {
            set
            {
                _data = value;

                CoinsReactive = new ReactiveProperty<int>(_data.Coins).AddTo(_disposables);
                LevelReactive = new ReactiveProperty<LevelInfo>(_data.Level).AddTo(_disposables);

                var menuInventoryData = _data.MenuBoosterInventory.ToDefault();
                var gameplayInventoryData = _data.GameplayBoosterInventory.ToDefault();

                MenuBoosterInventory = new BoosterInventoryData(menuInventoryData);
                GameplayBoosterInventory = new BoosterInventoryData(gameplayInventoryData);

                CoinsReactive
                    .Subscribe(newValue => _data.Coins = newValue)
                    .AddTo(_disposables);

                LevelReactive
                    .Subscribe(newValue => _data.Level = newValue)
                    .AddTo(_disposables);
            }
        }

        public PlayerProgress Serializable
        {
            get
            {
                _data.MenuBoosterInventory = MenuBoosterInventory.Boosters.ToList().ToSerializable();
                _data.GameplayBoosterInventory = GameplayBoosterInventory.Boosters.ToList().ToSerializable();

                return _data;
            }
        }

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

        public static PlayerProgress Default =>
            new()
            {
                Coins = 0,
                Level = LevelInfo.Default,
                MenuBoosterInventory = new BoosterInventoryData().Boosters.ToList().ToSerializable(),
                GameplayBoosterInventory = new BoosterInventoryData().Boosters.ToList().ToSerializable()
            };

        public void Dispose() => _disposables.Dispose();
    }
}