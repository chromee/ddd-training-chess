using System;
using Chess.Scripts.Domains.Movements;
using UnityEngine;

namespace Chess.Scripts.Domains.Pieces
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
        public static int Distance(Position a, Position b) => Mathf.CeilToInt(Mathf.Sqrt(Mathf.Pow(Mathf.Abs(a.X - b.X), 2) + Mathf.Pow(Mathf.Abs(a.Y - b.Y), 2)));


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

        public static MoveAmount operator -(Position a, Position b) => new(a.X - b.X, a.Y - b.Y);

        public static Position operator +(Position a, MoveAmount b) => new(a.X + b.X, a.Y + b.Y);
        public static Position operator -(Position a, MoveAmount b) => new(a.X - b.X, a.Y - b.Y);
    }
}
