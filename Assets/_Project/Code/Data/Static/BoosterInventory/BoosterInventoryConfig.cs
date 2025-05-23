using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;

namespace _Project.Code.Data.Static.BoosterInventory
{
    [System.Serializable]
    public struct BoosterInventoryConfig
    {
        public List<BoosterType> Cells;
        public List<CellBlockConfig> Blocks;
    }
    
    [System.Serializable]
    public struct CellBlockConfig
    {
        public int CellIndex;
        public int UnblockLevelIndex;
    }
}