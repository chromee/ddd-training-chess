using System;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using UnityEngine;
using DomainPieceType = Chess.Scripts.Domains.Pieces.PieceType;

namespace Chess.Scripts.Applications.Pieces
{
    public enum PieceColor { White, Black, }

    public enum PieceType { Pawn, Knight, Rook, Bishop, Queen, King, }

    public readonly struct PieceDto
    {
        public readonly PieceColor Color;
        public readonly PieceType Type;
        public readonly Vector2 Position;

        public PieceDto(PieceColor color, PieceType type, Vector2 position)
        {
            Color = color;
            Type = type;
            Position = position;
        }
    }

    public static class PieceExtensions
    {
        public static PieceDto ToToDto(this Piece p) => new(p.Color.ToDto(), p.Type.ToDto(), p.Position.ToVector2());

        public static PieceColor ToDto(this PlayerColor color) => color switch
        {
            PlayerColor.White => PieceColor.White,
            PlayerColor.Black => PieceColor.Black,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public static PieceType ToDto(this DomainPieceType type) => type switch
        {
            DomainPieceType.Pawn => PieceType.Pawn,
            DomainPieceType.Knight => PieceType.Knight,
            DomainPieceType.Rook => PieceType.Rook,
            DomainPieceType.Bishop => PieceType.Bishop,
            DomainPieceType.Queen => PieceType.Queen,
            DomainPieceType.King => PieceType.King,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };

        public static Vector2Int ToVector2(this Position position) => new(position.X, position.Y);
        public static Position ToPosition(this Vector2 position) => new((int)position.x, (int)position.y);

        public static DomainPieceType ToDomain(this PieceType type) => type switch
        {
            PieceType.Pawn => DomainPieceType.Pawn,
            PieceType.Knight => DomainPieceType.Knight,
            PieceType.Rook => DomainPieceType.Rook,
            PieceType.Bishop => DomainPieceType.Bishop,
            PieceType.Queen => DomainPieceType.Queen,
            PieceType.King => DomainPieceType.King,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }
}
