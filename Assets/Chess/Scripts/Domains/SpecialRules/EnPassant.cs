using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class EnPassant : SpecialRule
    {
        public override void TryExecute(Game game)
        {
            if (game.PieceMovementLogger.LastPieceMovement == null) return;
            var lastHand = game.PieceMovementLogger.LastPieceMovement.Value;

            if (game.PieceMovementLogger.SecondLastPieceMovement == null) return;
            var secondLastHand = game.PieceMovementLogger.SecondLastPieceMovement.Value;

            if (!secondLastHand.IsPawnTwoSpaceMove()) return;

            if (lastHand.NextPosition.X != secondLastHand.NextPosition.X) return;

            var attackerY = lastHand.MovedPieceColor == PlayerColor.White ? 5 : 2;
            var victimY = lastHand.MovedPieceColor == PlayerColor.White ? 4 : 3;
            if (lastHand.NextPosition.Y != attackerY || secondLastHand.NextPosition.Y != victimY) return;

            game.Board.RemovePiece(secondLastHand.NextPosition);
        }
    }
}
