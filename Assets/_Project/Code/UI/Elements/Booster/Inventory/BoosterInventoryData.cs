using System;
using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;

namespace _Project.Code.UI.Elements.Booster.Inventory
{
    [Serializable]
    public class BoosterInventoryData
    {
        public readonly Dictionary<BoosterId, int> Boosters;

        public BoosterInventoryData()
        {
            Boosters = new()
            {
                { new BoosterId(BoosterType.Bomb.ToString()), 1 },
                { new BoosterId(BoosterType.ComboCollect.ToString()), 1 },
                { new BoosterId(BoosterType.ReplaceItems.ToString()), 1 },
                { new BoosterId(BoosterType.TimeStop.ToString()), 1 },
                { new BoosterId(BoosterType.Shuffle.ToString()), 1 },
            };  
        }

        public BoosterInventoryData(List<KeyValuePair<BoosterId, int>> rawData) => 
            Boosters = new Dictionary<BoosterId, int>(rawData);
    }
}