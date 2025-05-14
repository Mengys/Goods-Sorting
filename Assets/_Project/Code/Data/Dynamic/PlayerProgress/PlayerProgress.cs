using System;

namespace _Project.Code.Data.Dynamic.PlayerProgress
{
    [Serializable]
    public class PlayerProgress
    {
        public int Coins = 0;
        public LevelInfo Level = LevelInfo.Default;
    }
}