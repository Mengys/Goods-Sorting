using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;
using UnityEngine;

namespace _Project.Code.UI.Windows.Implementations
{
    public interface IBoosterInventory
    {
        IReadOnlyDictionary<BoosterId, int> Boosters { get; }
        
        void Add(BoosterId id);
        bool Has(BoosterId id);
        void Remove(BoosterId id);
    }

    public class BoosterInventory : IBoosterInventory
    {
        private readonly Dictionary<BoosterId, int> _boosters;
        
        public BoosterInventory(Dictionary<BoosterId, int> boosters)
        {
            _boosters = boosters;
        }
        
        public BoosterInventory()
        {
            int count = 3;
            
            _boosters = new Dictionary<BoosterId, int>()
            {
                { new BoosterId(BoosterType.Bomb.ToString()), count },
                { new BoosterId(BoosterType.ComboCollect.ToString()), count },
                { new BoosterId(BoosterType.ReplaceItems.ToString()), count },
                { new BoosterId(BoosterType.TimeStop.ToString()), count },
                { new BoosterId(BoosterType.Shuffle.ToString()), count },
            };
        }

        public IReadOnlyDictionary<BoosterId, int> Boosters => _boosters;

        public void Add(BoosterId id) => 
            _boosters[id] = _boosters.GetValueOrDefault(id) + 1;
        
        public bool Has(BoosterId id) => 
            _boosters.TryGetValue(id, out var count) && count > 0;
        
        public void Remove(BoosterId id)
        {
            if (_boosters.TryGetValue(id, out var count))
                _boosters[id] = Mathf.Max(0, count - 1);
        }
    }
}