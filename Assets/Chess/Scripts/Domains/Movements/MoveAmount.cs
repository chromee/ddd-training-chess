using System;

namespace Chess.Scripts.Domains.Movements
{
    public readonly struct MoveAmount : IEquatable<MoveAmount>
    {
        public readonly int X;
        public readonly int Y;

        public MoveAmount(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MoveAmount Normalized() => new(X == 0 ? 0 : X > 0 ? 1 : -1, Y == 0 ? 0 : Y > 0 ? 1 : -1);

        public override string ToString() => $"({X}, {Y})";

        public bool Equals(MoveAmount other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is MoveAmount other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(MoveAmount a, MoveAmount b) => a.Equals(b);
        public static bool operator !=(MoveAmount a, MoveAmount b) => !a.Equals(b);
        public static MoveAmount operator +(MoveAmount a, MoveAmount b) => new(a.X + b.X, a.Y + b.Y);
        public static MoveAmount operator -(MoveAmount a, MoveAmount b) => new(a.X - b.X, a.Y - b.Y);
        public static MoveAmount operator *(MoveAmount a, int b) => new(a.X * b, a.Y * b);
    }
}
