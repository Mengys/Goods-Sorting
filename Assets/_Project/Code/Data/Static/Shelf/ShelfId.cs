using System;

namespace _Project.Code.Data.Static.Shelf
{
    [Serializable]
    public struct ShelfId : IEquatable<ShelfId>
    {
        public string Value;

        public ShelfId(string id) =>
            Value = id;

        public const string Default = "Default";

        public bool Equals(ShelfId other) =>
            Value == other.Value;

        public override bool Equals(object obj) =>
            obj is ShelfId other && Equals(other);

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() =>
            Value.ToString();
    }
}