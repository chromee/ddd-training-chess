using System;
using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.Movements
{
    public readonly struct Movement : IEquatable<Movement>
    {
        public readonly int X;
        public readonly int Y;

        public Movement(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Movement Normalized()
        {
            return new Movement(X == 0 ? 0 : X > 0 ? 1 : -1, Y == 0 ? 0 : Y > 0 ? 1 : -1);
        }

        public Movement ConsiderColor(Player player)
        {
            return new Movement(X, player.Color == PlayerColor.White ? Y : -Y);
        }

        public bool Equals(Movement other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is Movement other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Movement a, Movement b) => a.Equals(b);
        public static bool operator !=(Movement a, Movement b) => !a.Equals(b);
        public static Movement operator +(Movement a, Movement b) => new(a.X + b.X, a.Y + b.Y);
        public static Movement operator -(Movement a, Movement b) => new(a.X - b.X, a.Y - b.Y);
        public static Movement operator *(Movement a, int b) => new(a.X * b, a.Y * b);
    }
}
