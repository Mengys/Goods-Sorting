using UnityEngine;

namespace _Project.Code
{
    [System.Serializable]
    public struct LevelConfig
    {
        public ShelfConfig[] Shelves;
    }

    [System.Serializable]
    public struct ShelfConfig
    {
        public string Type;
        public Vector3 Position;
        public string[] Items;
    }

    [System.Serializable]
    public struct ItemConfig
    {
        public string Id;
        public string Name;
    }

    [System.Serializable]
    public struct ShelfLayerConfig
    {
        public ItemConfig[] Items;
    }
}