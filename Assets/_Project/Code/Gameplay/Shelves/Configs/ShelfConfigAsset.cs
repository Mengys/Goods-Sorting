using System;
using _Project.Code.Gameplay.Grid.Config;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Code.Gameplay.Shelves.Configs
{
    [Serializable]
    public struct ShelfConfigAsset
    {
        public string Id;
        public Vector2 Position;
        public ItemGridAssetConfig ItemGridAssetConfig;
    }
}