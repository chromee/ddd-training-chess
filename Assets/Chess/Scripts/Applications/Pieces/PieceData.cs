using System;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public enum PieceColor { White, Black, }

    public enum PieceType { Pawn, Knight, Rook, Bishop, Queen, King, }

    public struct PieceData
    {
        public readonly PieceColor Color;
        public readonly PieceType Type;
        public readonly Vector2 Position;

        public PieceData(PieceColor color, PieceType type, Vector2 position)
        {
            Color = color;
            Type = type;
            Position = position;
        }
    }

    public static class PieceExtensions
    {
        public static PieceData ToData(this Piece p) => new(p.Color.ToDto(), p.Type.ToDto(), p.Position.ToVector2());

        public static PieceColor ToDto(this PlayerColor color) => color switch
        {
            PlayerColor.White => PieceColor.White,
            PlayerColor.Black => PieceColor.Black,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public static PieceType ToDto(this Domains.Pieces.PieceType type) => type switch
        {
            Domains.Pieces.PieceType.Pawn => PieceType.Pawn,
            Domains.Pieces.PieceType.Knight => PieceType.Knight,
            Domains.Pieces.PieceType.Rook => PieceType.Rook,
            Domains.Pieces.PieceType.Bishop => PieceType.Bishop,
            Domains.Pieces.PieceType.Queen => PieceType.Queen,
            Domains.Pieces.PieceType.King => PieceType.King,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };

        public static Vector2 ToVector2(this Position position) => new(position.X, position.Y);
        public static Position ToPosition(this Vector2 position) => new((int)position.x, (int)position.y);

        public static Domains.Pieces.PieceType ToDomain(this PieceType type) => type switch
        {
            PieceType.Pawn => Domains.Pieces.PieceType.Pawn,
            PieceType.Knight => Domains.Pieces.PieceType.Knight,
            PieceType.Rook => Domains.Pieces.PieceType.Rook,
            PieceType.Bishop => Domains.Pieces.PieceType.Bishop,
            PieceType.Queen => Domains.Pieces.PieceType.Queen,
            PieceType.King => Domains.Pieces.PieceType.King,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }
}
