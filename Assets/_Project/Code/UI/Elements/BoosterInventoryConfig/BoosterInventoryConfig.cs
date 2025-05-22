using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Gameplay.Items;

namespace _Project.Code.UI.Elements
{
    [System.Serializable]
    public struct BoosterInventoryConfig
    {
        public List<BoosterType> Cells;
        public List<CellBlockConfig> Blocks;
        public List<CellFillConfig> Fills;
    }

    [System.Serializable]
    public struct CellFillConfig
    {
        public int CellIndex;
        public int Amount;
        public int MaxAmount;
        
        public List<int> FillLevelIndexes;
    }

    [System.Serializable]
    public struct CellBlockConfig
    {
        public int CellIndex;
        public int UnblockLevelIndex;
    }
}