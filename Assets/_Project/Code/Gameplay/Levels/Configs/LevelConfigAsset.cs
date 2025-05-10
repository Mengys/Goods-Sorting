using _Project.Code.Gameplay.Grid.Config;
using UnityEngine;

namespace _Project.Code.Gameplay.Levels.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfigAsset : ScriptableObject
    {
        public DifficultyType Difficulty;
        public GridConfigAsset Grid;
    }

    public struct LevelConfig
    {
        public DifficultyType Difficulty;
        public GridConfig Grid;
    }
}