using System;

namespace Chess.Domain.ValueObjects
{
    public readonly struct Row : IEquatable<Row>
    {
        public readonly int Value;
        private const int MinValue = 0;
        private const int MaxValue = 7;

        public Row(int value)
        {
            if (value < MinValue || MaxValue < value) throw new ArgumentOutOfRangeException();
            Value = value;
        }

        public int GetDiffFromMaxValue() => MaxValue - Value;
        public int GetDiffFromMinValue() => Value - MinValue;

        public bool Equals(Row other) => Value == other.Value;

        public override bool Equals(object obj) => obj is Row other && Equals(other);

        public override int GetHashCode() => Value;

        public static implicit operator Row(int v) => new Row(v);

        public static bool operator ==(Row a, Row b) => a.Equals(b);
        public static bool operator !=(Row a, Row b) => !a.Equals(b);
        public static Row operator +(Row a, Row b) => a.Value + b.Value;
        public static Row operator -(Row a, Row b) => a.Value - b.Value;
    }
}
