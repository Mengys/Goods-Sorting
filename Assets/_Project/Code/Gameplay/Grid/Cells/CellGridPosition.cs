using System;

namespace _Project.Code.Gameplay.Grid.Cells
{
    public struct CellGridPosition : IEquatable<CellGridPosition>
    {
        public int Shelf;
        public int Layer;
        public int Column;

        public CellGridPosition(int shelf, int layer, int column)
        {
            Shelf = shelf;
            Layer = layer;
            Column = column;
        }

        public bool Equals(CellGridPosition other) =>
            Shelf == other.Shelf && Layer == other.Layer && Column == other.Column;

        public override bool Equals(object obj) =>
            obj is CellGridPosition other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(Shelf, Layer, Column);
    }
}