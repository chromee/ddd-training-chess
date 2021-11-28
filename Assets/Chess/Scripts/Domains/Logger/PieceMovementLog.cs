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
        public readonly bool IsKillPiece;

        public PieceMovementLog(Piece movedPiece, Position prevPosition, Position nextPosition, bool isKillPiece)
        {
            MovedPieceColor = movedPiece.Color;
            MovedPieceType = movedPiece.Type;
            PrevPosition = prevPosition;
            NextPosition = nextPosition;
            IsKillPiece = isKillPiece;
        }

        public bool IsPawnTwoSpaceMove()
        {
            if (MovedPieceType != PieceType.Pawn) return false;
            return Math.Abs(PrevPosition.Y - NextPosition.Y) == 2;
        }
    }
}
