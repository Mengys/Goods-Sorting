using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Grid.Config;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Shelves;
using UnityEngine;

namespace _Project.Code.Gameplay.Levels
{
    [Serializable]
    public struct LevelDataConfig
    {
        public DifficultyType Difficulty;
        public GridConfig gridConfig;
    }
    
    [Serializable]
    public struct ItemsLayerConfig
    {
        public List<ItemId> Items;
    }

    public struct ShelfData
    {
        public ShelfId Id;
        public Dictionary<int, Vector3> Places;
        public Dictionary<ItemPosition, ItemData?> Items;
    }
    
    public struct ItemPosition
    {
        public int Layer;
        public int Index;
    }

    public struct ItemData
    {
        public ItemId Id;
    }
}