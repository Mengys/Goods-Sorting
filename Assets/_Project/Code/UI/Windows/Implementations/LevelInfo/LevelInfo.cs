using System;
using _Project.Code.Gameplay.Levels;

namespace _Project.Code.UI.Windows.Implementations.LevelInfo
{
    [Serializable]
    public struct LevelInfo
    {
        public int Number;
        public DifficultyType Difficulty;
        
        public int Id => Number - 1;
        
        public static LevelInfo Default => new()
        {
            Number = 1,
            Difficulty = DifficultyType.Easy
        };
    }
}