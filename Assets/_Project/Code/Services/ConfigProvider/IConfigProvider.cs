using _Project.Code.Data.Static.Booster;
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
using _Project.Code.UI.Windows.Base;

namespace _Project.Code.Services.ConfigProvider
{
    public interface IConfigProvider
    {
        WindowConfig? ForWindow(WindowId id);
        GameStateConfig? ForState(GameStateId id);
        ParticleConfig? ForParticle(ParticleId id);
        SoundConfig? ForSound(SoundId id);
        
        LevelConfig? ForLevel(int id);
        ShelfPrefabConfig? ForShelf(ShelfId shelfId);
        ItemConfig? ForItem(ItemId itemId);
        
        BoosterConfig? ForBooster(BoosterId id);
        
        ItemView PrefabForItem(ItemId itemId);
        ItemView PreviewPrefabForItem(ItemId id);
        void ValidateIds(GridConfig config);
        int WinAdCoinsMultiplier { get; }
        ScoreIncomeConfig? ScoreIncomeConfig { get; }
    }
}