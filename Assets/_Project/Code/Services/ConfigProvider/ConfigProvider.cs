using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Services;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Data.Static.Game;
using _Project.Code.Data.Static.GameState;
using _Project.Code.Data.Static.Grid;
using _Project.Code.Data.Static.Item;
using _Project.Code.Data.Static.Level;
using _Project.Code.Data.Static.Particles;
using _Project.Code.Data.Static.ScoreIncome;
using _Project.Code.Data.Static.Shelf;
using _Project.Code.Data.Static.Sound;
using _Project.Code.Data.Static.Windows;
using _Project.Code.Gameplay.Boosters.Configs;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Services.AssetsLoading;
using _Project.Code.UI.Windows;
using _Project.Code.UI.Windows.Base;
using UnityEngine;

namespace _Project.Code.Services.ConfigProvider
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly Dictionary<GameStateId, GameStateConfig> _gameStateConfigs;
        private readonly Dictionary<WindowId, WindowConfig> _windowConfigs;

        private readonly Dictionary<ParticleId, ParticleConfig> _particleConfigs;
        private readonly Dictionary<SoundId, SoundConfig> _soundConfigs;

        private readonly Dictionary<int, LevelConfig> _levelConfigs;
        private readonly Dictionary<ShelfId, ShelfPrefabConfig> _shelfConfigs;
        private readonly Dictionary<ItemId, ItemConfig> _itemConfigs;
        
        private readonly Dictionary<BoosterId, BoosterConfig> _boosterConfigs;

        private readonly ItemView _itemPrefab;

        public static ConfigProvider NewInstance =>
            new(Resources.Load<GameConfig>(ResourcesPaths.GameConfig));

        public ConfigProvider(GameConfig config)
        {
            _gameStateConfigs = config
                .GameStateListConfig
                .States
                .ToDictionary(x => x.Id, x => x);

            _windowConfigs = config
                .WindowConfigList
                .Windows
                .ToDictionary(x => x.Id, x => x);

            _particleConfigs = config
                .ParticlesConfig
                .ParticleConfigs
                .ToDictionary(x => x.Id, x => x);

            _soundConfigs = config
                .SoundListConfig
                .Sounds
                .ToDictionary(x => x.Id, x => x);

            _shelfConfigs = config
                .ShelfPrefabConfigList
                .Shelves
                .ToDictionary(x => x.Id, x => x);

            _itemConfigs = config
                .ItemConfigList
                .Configs
                .ToDictionary(x => x.Id, x => x);

            _levelConfigs = config
                .LevelConfigList
                .Configs
                .Select(AssetDataFormatter.AsLevelConfig)
                .Select((item, index) => new { item, index })
                .ToDictionary(x => x.index, x => x.item);

            _boosterConfigs = config
                .BoosterConfigList
                .Configs
                .ToDictionary(x => x.Id, x => x);

            _itemPrefab = config.ItemConfigList.ItemPrefab;
            
            ScoreIncomeConfig = config.ScoreIncomeConfig.Config;
        }

        public WindowConfig? ForWindow(WindowId id) =>
            _windowConfigs.TryGetValue(id, out var config) ? config : null;

        public GameStateConfig? ForState(GameStateId id) =>
            _gameStateConfigs.TryGetValue(id, out var config) ? config : throw new KeyNotFoundException(id.ToString());

        public ParticleConfig? ForParticle(ParticleId id) =>
            _particleConfigs.TryGetValue(id, out var config) ? config : throw new KeyNotFoundException(id.ToString());

        public SoundConfig? ForSound(SoundId id) =>
            _soundConfigs.TryGetValue(id, out var config) ? config : throw new KeyNotFoundException(id.ToString());

        public LevelConfig? ForLevel(int id) =>
            _levelConfigs.TryGetValue(id, out var config) ? config :
                null;

        public ShelfPrefabConfig? ForShelf(ShelfId shelfId) =>
            _shelfConfigs.TryGetValue(shelfId, out var config)
                ? config
                : null;

        public ItemConfig? ForItem(ItemId itemId) =>
            _itemConfigs.TryGetValue(itemId, out var config)
                ? config
                : null;

        public ItemView PrefabForItem(ItemId itemId) =>
            _itemPrefab;

        public ItemView PreviewPrefabForItem(ItemId id) =>
            _itemPrefab;

        public BoosterConfig? ForBooster(BoosterId id) =>
            _boosterConfigs.TryGetValue(id, out var config)
                ? config
                : null;

        public void ValidateIds(GridConfig config)
        {
            for (int i = 0; i < config.ShelvesCount; i++)
            {
                var id = config.GetShelfId(i);

                var shelfPrefabConfig = ForShelf(id);

                if (shelfPrefabConfig.HasValue == false)
                    throw new KeyNotFoundException($"Config for shelf Id \"{id}\" not found");

                var itemGrid = config.GetItemGrid(i);

                foreach (var itemId in itemGrid.Items)
                {
                    if (itemId == null) continue;

                    var itemConfig = ForItem(itemId.Value);

                    if (itemConfig.HasValue == false)
                        throw new KeyNotFoundException($"Config for item Id \"{itemId.Value}\" not found");
                }
            }
        }

        public int WinAdCoinsMultiplier => 5;
        public ScoreIncomeConfig? ScoreIncomeConfig { get; }
    }
}