using System;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnEnPassantCondition : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            var prevHand = board.LastPieceMovement;
            if (prevHand == null) return false;
            if (prevHand.MovedPiece.IsAlly(piece)) return false;
            if (!prevHand.IsPawnTwoSpaceMove()) return false;
            var prevNextMidPosition = new Position(prevHand.NextPosition.X, (prevHand.PrevPosition.Y + prevHand.NextPosition.Y) / 2);
            return destination == prevNextMidPosition;
        }
    }
}
