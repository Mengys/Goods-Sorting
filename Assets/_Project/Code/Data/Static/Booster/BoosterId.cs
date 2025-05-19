using _Project.Code.Gameplay.Items;

namespace _Project.Code.Data.Static.Booster
{
    public struct BoosterId
    {
        public string Id;

        public BoosterId(string id) => Id = id;

        public bool Equals(BoosterId other) => 
            Id == other.Id;

        public override bool Equals(object obj) => 
            obj is BoosterId other && Equals(other);

        public override int GetHashCode() =>
            Id?.GetHashCode() ?? 0;

        public override string ToString() => Id;
        
        public static bool operator ==(BoosterId left, BoosterId right) => 
            left.Equals(right);

        public static bool operator !=(BoosterId left, BoosterId right) => 
            !left.Equals(right);
    }
}