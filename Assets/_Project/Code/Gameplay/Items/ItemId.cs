using System;

namespace _Project.Code.Gameplay.Items
{
    [Serializable]
    public struct ItemId : IEquatable<ItemId>
    {
        public string Id;

        public ItemId(string id) => Id = id;

        public bool Equals(ItemId other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
        
        public override string ToString() => Id;
    }
}