using _Project.Code.Gameplay.Items.Configs;
using _Project.Code.Gameplay.Levels.Configs;
using _Project.Code.Gameplay.Shelves.Configs;
using _Project.Code.Infrastructure.GameStateMachine.Config;
using _Project.Code.Services.Factories.UI.Config;
using _Project.Code.Services.ParticlesPlayer.Config;
using _Project.Code.Services.SoundPlayer.Config;
using UnityEngine;

namespace _Project.Code.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public GameStatesConfig GameStatesConfig { get; private set; }
        [field: SerializeField] public WindowConfigList WindowConfigList { get; private set; }
        
        [field: Header("FX")]
        [field: SerializeField] public ParticlesConfig ParticlesConfig { get; private set; }
        [field: SerializeField] public SoundsConfig SoundsConfig { get; private set; }
        
        [field: Header("Gameplay")]
        [field: SerializeField] public LevelConfigList LevelConfigList { get; set; }
        [field: SerializeField] public ShelfPrefabConfigList ShelfPrefabConfigList { get; set; }
        [field: SerializeField] public ItemConfigList ItemConfigList { get; set; }
    }
}