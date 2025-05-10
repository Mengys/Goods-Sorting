using System.Collections.Generic;
using _Project.Code.Gameplay.Shelves.Configs;

namespace _Project.Code.Gameplay.Grid.Config
{
    [System.Serializable]
    public struct GridConfigAsset
    {
        public List<ShelfConfigAsset> Shelves;
    }
}