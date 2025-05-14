using System;
using _Project.Code.Data.Static.Grid;
using UnityEngine;

namespace _Project.Code.Data.Static.Shelf
{
    [Serializable]
    public struct ShelfConfigAsset
    {
        public string Id;
        public Vector2 Position;
        public ItemGridAssetConfig ItemGridAssetConfig;
    }
}