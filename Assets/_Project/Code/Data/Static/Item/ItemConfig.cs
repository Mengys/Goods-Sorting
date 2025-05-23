using System;
using _Project.Code.Gameplay.Items;
using UnityEngine;

namespace _Project.Code.Data.Static.Item
{
    [Serializable]
    public struct ItemConfig
    {
        public string StrId;
        public Sprite Sprite;
        
        public ItemId Id => new(StrId);
    }
}