using System;
using _Project.Code.Data.Static.Level;

namespace _Project.Code.Data.Dynamic
{
    [Serializable]
    public struct LevelInfo
    {
        public int Number => Id + 1;
        
        public DifficultyType Difficulty;
        public int Id;
        
        public static LevelInfo Default => new()
        {
            Id = 0,
            Difficulty = DifficultyType.Easy
        };
    }
}