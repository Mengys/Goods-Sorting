using _Project.Code.Gameplay.Grid.Config;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Items.Configs;
using _Project.Code.Gameplay.Levels.Configs;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Gameplay.Shelves.Configs;
using _Project.Code.Infrastructure.GameStateMachine.Config;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.Factories.UI.Config;
using _Project.Code.Services.ParticlesPlayer.Config;
using _Project.Code.Services.SoundPlayer.Config;
using _Project.Code.UI.Windows;

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
        
        ItemView PrefabForItem(ItemId itemId);
        ItemView PreviewPrefabForItem(ItemId id);
        void ValidateIds(GridConfig config);
    }
}