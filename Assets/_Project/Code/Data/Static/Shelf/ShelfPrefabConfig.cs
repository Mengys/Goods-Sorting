using System;
using _Project.Code.Gameplay.Shelves;

namespace _Project.Code.Data.Static.Shelf
{
    [Serializable]
    public struct ShelfPrefabConfig
    {
        public string StrId;
        public ShelfView Prefab;

        public ShelfPrefabConfig(string strId, ShelfView prefab)
        {
            StrId = strId;
            Prefab = prefab;
        }

        public ShelfId Id => new(StrId);
    }
}