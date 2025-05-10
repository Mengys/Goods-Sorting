using System;

namespace _Project.Code.Gameplay.Shelves.Configs
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