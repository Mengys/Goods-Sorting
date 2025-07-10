using _Project.Code.Data.Static.Grid;
using UnityEngine;

namespace _Project.Code.Data.Static.Level
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