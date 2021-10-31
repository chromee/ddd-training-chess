using System;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Boards
{
    public class PieceMovementLog
    {
        public readonly Piece MovedPiece;
        public readonly Position PrevPosition;
        public readonly Position NextPosition;

        public PieceMovementLog(Piece movedPiece, Position prevPosition, Position nextPosition)
        {
            MovedPiece = movedPiece;
            PrevPosition = prevPosition;
            NextPosition = nextPosition;
        }

        public bool IsPawnTwoSpaceMove()
        {
            if (!MovedPiece.IsType(PieceType.Pawn)) return false;
            return Math.Abs(PrevPosition.Y - NextPosition.Y) == 2;
        }
    }
}
