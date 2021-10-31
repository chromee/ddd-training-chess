using System;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.HandLogs
{
    public class HandLog
    {
        public readonly Piece MovedPiece;
        public readonly Position PrevPosition;
        public readonly Position NextPosition;

        public HandLog(Piece movedPiece, Position prevPosition, Position nextPosition)
        {
            MovedPiece = movedPiece;
            PrevPosition = prevPosition;
            NextPosition = nextPosition;
        }

        public bool IsPawnTwoSpaceMove()
        {
            if (!MovedPiece.IsSameType(PieceType.Pawn)) return false;
            return Math.Abs(PrevPosition.Y - NextPosition.Y) == 2;
        }
    }
}
