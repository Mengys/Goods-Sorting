using System;
using System.Collections.Generic;
using _Project.Code.Data.Dynamic.PlayerProgress.SerializableKeyValue;
using _Project.Code.Data.Static.Booster;

namespace _Project.Code.Data.Dynamic.PlayerProgress
{
    [Serializable]
    public class PlayerProgress
    {
        public int Coins = 0;
        public LevelInfo Level = LevelInfo.Default;
        
        public List<SerializableKeyValuePair<BoosterId, int>> MenuBoosterInventory = new();
        public List<SerializableKeyValuePair<BoosterId, int>> GameplayBoosterInventory = new();
    }
}