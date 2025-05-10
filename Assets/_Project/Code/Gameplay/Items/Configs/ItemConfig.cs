using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Items.Configs
{
    [Serializable]
    public struct ItemConfig
    {
        public string StrId;
        public Sprite Sprite;
        
        public ItemId Id => new(StrId);
    }
}