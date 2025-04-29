using _Project.Code.Gameplay.Level;

namespace _Project.Code.UI.Window.Implementations.LevelInfo
{
    public class LevelInfoWindowModel
    {
        public int Level { get; private set; }
        public DifficultyType Difficulty { get; private set; }
        
        public LevelInfoWindowModel(int level, DifficultyType difficulty)
        {
            Level = level;
            Difficulty = difficulty;
        }
    }
}