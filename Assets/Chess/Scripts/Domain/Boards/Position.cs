using System;
using Chess.Domain.Movements;

namespace Chess.Domain.Boards
{
    public readonly struct Position : IEquatable<Position>
    {
        public readonly int X;
        public readonly int Y;
        private const int MinValue = 0;
        private const int MaxValue = 7;

        public Position(int x, int y)
        {
            if (x < MinValue || MaxValue < x) throw new ArgumentOutOfRangeException($"X({x}) is out of range.");
            if (y < MinValue || MaxValue < y) throw new ArgumentOutOfRangeException($"Y({y}) is out of range.");
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";


        public bool Equals(Position other) => Y.Equals(other.Y) && X.Equals(other.X);

        public override bool Equals(object obj) => obj is Position other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (Y.GetHashCode() * 397) ^ X.GetHashCode();
            }
        }

        public static bool operator ==(Position a, Position b) => a.Equals(b);
        public static bool operator !=(Position a, Position b) => !a.Equals(b);

        public static Movement operator -(Position a, Position b) => new Movement(a.X - b.X, a.Y - b.Y);

        public static Position operator +(Position a, Movement b) => new(a.X + b.X, a.Y + b.Y);
        public static Position operator -(Position a, Movement b) => new(a.X - b.X, a.Y - b.Y);
    }
}
