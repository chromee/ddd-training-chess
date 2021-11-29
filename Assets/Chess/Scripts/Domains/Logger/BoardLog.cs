using System;
using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Logger
{
    public readonly struct BoardLog : IEquatable<BoardLog>
    {
        public readonly PieceLog[] PieceLogs;

        public BoardLog(PieceLog[] pieceLogs)
        {
            PieceLogs = pieceLogs;
        }

        public bool Equals(BoardLog other)
        {
            if (PieceLogs == null || other.PieceLogs == null) return false;
            if (PieceLogs.Length != other.PieceLogs.Length) return false;

            for (var i = 0; i < PieceLogs.Length; i++)
            {
                if (PieceLogs[i] != other.PieceLogs[i]) return false;
            }

            return true;
        }
        public static bool operator ==(BoardLog a, BoardLog b) => a.Equals(b);
        public static bool operator !=(BoardLog a, BoardLog b) => !a.Equals(b);
    }

    public readonly struct PieceLog : IEquatable<PieceLog>
    {
        public readonly PlayerColor Color;
        public readonly PieceType Type;
        public readonly Position Position;

        public PieceLog(PlayerColor color, PieceType type, Position position)
        {
            Color = color;
            Type = type;
            Position = position;
        }

        public bool Equals(PieceLog other) => Color == other.Color && Type == other.Type && Position.Equals(other.Position);
        public override bool Equals(object obj) => obj is PieceLog other && Equals(other);
        public override int GetHashCode() => HashCode.Combine((int)Color, (int)Type, Position);
        public static bool operator ==(PieceLog a, PieceLog b) => a.Equals(b);
        public static bool operator !=(PieceLog a, PieceLog b) => !a.Equals(b);
    }
}
