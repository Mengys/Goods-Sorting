using System;

namespace _Project.Code.Gameplay.Grid.Config
{
    [Serializable]
    public class ItemGridAssetConfig
    {
        public int Rows = 1;
        public int Columns = 0;
        public string[] Cells;
    }
}