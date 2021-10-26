using System;
using Chess.Domain.Boards;
using Chess.Domain.Games;
using Chess.Domain.Pieces;
using UnityEngine;

namespace Chess.Application.Dto
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
        public static PieceData ToData(this Piece p) => new PieceData(p.Color.ToDto(), p.Type.ToDto(), p.Position.ToVector2());

        public static PieceColor ToDto(this PlayerColor color) => color switch
        {
            PlayerColor.White => PieceColor.White,
            PlayerColor.Black => PieceColor.Black,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public static PieceType ToDto(this Chess.Domain.Pieces.PieceType type) => type switch
        {
            Domain.Pieces.PieceType.Pawn => PieceType.Pawn,
            Domain.Pieces.PieceType.Knight => PieceType.Knight,
            Domain.Pieces.PieceType.Rook => PieceType.Rook,
            Domain.Pieces.PieceType.Bishop => PieceType.Bishop,
            Domain.Pieces.PieceType.Queen => PieceType.Queen,
            Domain.Pieces.PieceType.King => PieceType.King,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        public static Vector2 ToVector2(this Position position) => new Vector2(position.X, position.Y);
        public static Position ToPosition(this Vector2 position) => new Position((int)position.x, (int)position.y);
    }
}
