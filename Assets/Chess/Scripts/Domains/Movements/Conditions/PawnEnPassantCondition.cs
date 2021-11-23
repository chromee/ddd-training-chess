using System;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnEnPassantCondition : IMovementCondition
    {
        public bool CanExecute(Game game, Piece piece, Position destination)
        {
            if (game.Logger.LastPieceMovement == null) return false;
            var prevHand = game.Logger.LastPieceMovement.Value;

            if (piece.IsColor(prevHand.MovedPieceColor)) return false;
            if (!prevHand.IsPawnTwoSpaceMove()) return false;

            var prevNextMidPosition = new Position(prevHand.NextPosition.X, (prevHand.PrevPosition.Y + prevHand.NextPosition.Y) / 2);
            return destination == prevNextMidPosition;
        }
    }
}
