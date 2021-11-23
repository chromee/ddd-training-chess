using System;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Logger
{
    public readonly struct PieceMovementLog
    {
        public readonly PlayerColor MovedPieceColor;
        public readonly PieceType MovedPieceType;
        public readonly Position PrevPosition;
        public readonly Position NextPosition;

        public PieceMovementLog(Piece movedPiece, Position prevPosition, Position nextPosition)
        {
            MovedPieceColor = movedPiece.Color;
            MovedPieceType = movedPiece.Type;
            PrevPosition = prevPosition;
            NextPosition = nextPosition;
        }

        public bool IsPawnTwoSpaceMove()
        {
            if (MovedPieceType != PieceType.Pawn) return false;
            return Math.Abs(PrevPosition.Y - NextPosition.Y) == 2;
        }
    }
}
