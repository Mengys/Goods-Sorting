namespace _Project.Code.Data.Services
{
    public static class DataTextFormatter
    {
        public static string Timer(float seconds) =>
            seconds.ToString("0");
        
        public static string Score(int score) =>
            score.ToString();
        public static string LevelShort(int levelNumber) =>
            $"Lv.{levelNumber}";
        
        public static string Level(int levelNumber) =>
            $"Level {levelNumber}";
    }
}