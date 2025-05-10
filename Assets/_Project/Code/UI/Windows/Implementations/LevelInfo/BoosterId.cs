using _Project.Code.Gameplay.Items;

namespace _Project.Code.UI.Windows.Implementations.LevelInfo
{
    public struct BoosterId
    {
        public string Id;

        public BoosterId(string id) => Id = id;

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