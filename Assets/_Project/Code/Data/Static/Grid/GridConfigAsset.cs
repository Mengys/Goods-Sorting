using System.Collections.Generic;
using _Project.Code.Data.Static.Shelf;

namespace _Project.Code.Data.Static.Grid
{
    [System.Serializable]
    public struct GridConfigAsset
    {
        public List<ShelfConfigAsset> Shelves;
    }
}