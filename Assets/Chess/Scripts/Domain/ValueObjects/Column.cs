using System;

namespace Chess.Domain.ValueObjects
{
    public readonly struct Column : IEquatable<Column>
    {
        public readonly int Value;
        private const int MinValue = 0;
        private const int MaxValue = 7;

        public Column(int value)
        {
            if (value < MinValue || MaxValue < value) throw new ArgumentOutOfRangeException();
            Value = value;
        }

        public int GetDiffFromMaxValue() => MaxValue - Value;
        public int GetDiffFromMinValue() => Value - MinValue;

        public bool Equals(Column other) => Value == other.Value;

        public override bool Equals(object obj) => obj is Column other && Equals(other);

        public override int GetHashCode() => Value;

        public static implicit operator Column(int v) => new Column(v);

        public static bool operator ==(Column a, Column b) => a.Equals(b);
        public static bool operator !=(Column a, Column b) => !a.Equals(b);
        public static Column operator +(Column a, Column b) => a.Value + b.Value;
        public static Column operator -(Column a, Column b) => a.Value - b.Value;
    }
}
