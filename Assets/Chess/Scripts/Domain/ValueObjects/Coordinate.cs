using System;

namespace Chess.Domain.ValueObjects
{
    public readonly struct Coordinate : IEquatable<Coordinate>
    {
        public readonly Column Column;
        public readonly Row Row;

        public Coordinate(Column column, Row row)
        {
            Column = column;
            Row = row;
        }

        public bool Equals(Coordinate other) => Row.Equals(other.Row) && Column.Equals(other.Column);

        public override bool Equals(object obj) => obj is Coordinate other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row.GetHashCode() * 397) ^ Column.GetHashCode();
            }
        }

        public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);
        public static bool operator !=(Coordinate a, Coordinate b) => !a.Equals(b);

        public static Coordinate operator +(Coordinate a, Coordinate b) =>
            new Coordinate(a.Column + b.Column, a.Row + b.Row);

        public static Coordinate operator -(Coordinate a, Coordinate b) =>
            new Coordinate(a.Column - b.Column, a.Row - b.Row);
    }
}
