using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements
{
    public class PawnEnPassantMovement : Movement
    {
        public PawnEnPassantMovement(MoveAmount moveAmount)
        {
            Movements = new[] { moveAmount, };
        }

        public override bool CanExecute(Game game, Piece piece, Position destination)
        {
            if (game.PieceMovementLogger.LastPieceMovement == null) return false;
            var prevHand = game.PieceMovementLogger.LastPieceMovement.Value;

            if (piece.IsColor(prevHand.MovedPieceColor)) return false;
            if (!prevHand.IsPawnTwoSpaceMove()) return false;

            var prevNextMidPosition = new Position(prevHand.NextPosition.X, (prevHand.PrevPosition.Y + prevHand.NextPosition.Y) / 2);
            return destination == prevNextMidPosition;
        }
    }
}
