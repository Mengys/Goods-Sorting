using _Project.Code.Data.Static.GameState;
using _Project.Code.Data.Static.Item;
using _Project.Code.Data.Static.Level;
using _Project.Code.Data.Static.Particles;
using _Project.Code.Data.Static.Shelf;
using _Project.Code.Data.Static.Sound;
using _Project.Code.Data.Static.Windows;
using _Project.Code.Gameplay.Boosters.Configs;
using UnityEngine;

namespace _Project.Code.Data.Static.Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public GameStateListConfig GameStateListConfig { get; private set; }
        [field: SerializeField] public WindowConfigList WindowConfigList { get; private set; }

        [field: Header("FX")]
        [field: SerializeField]
        public ParticlesConfig ParticlesConfig { get; private set; }

        [field: SerializeField] public SoundListConfig SoundListConfig { get; private set; }

        [field: Header("Gameplay")]
        [field: SerializeField] public LevelConfigList LevelConfigList { get; private set; }
        [field: SerializeField] public ShelfPrefabConfigList ShelfPrefabConfigList { get; private set; }
        [field: SerializeField] public ItemConfigList ItemConfigList { get; private set; }
        [field: SerializeField] public BoosterConfigList BoosterConfigList { get; private set; }
    }
}