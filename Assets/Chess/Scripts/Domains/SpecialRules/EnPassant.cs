using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class EnPassant : ISpecialRule
    {
        public void TryExecute(Game game)
        {
            if (game.Logger.LastPieceMovement == null) return;
            var lastHand = game.Logger.LastPieceMovement.Value;

            if (game.Logger.SecondLastPieceMovement == null) return;
            var secondLastHand = game.Logger.SecondLastPieceMovement.Value;

            if (!secondLastHand.IsPawnTwoSpaceMove()) return;

            if (lastHand.NextPosition.X != secondLastHand.NextPosition.X) return;

            var attackerY = lastHand.MovedPieceColor == PlayerColor.White ? 5 : 2;
            var victimY = lastHand.MovedPieceColor == PlayerColor.White ? 4 : 3;
            if (lastHand.NextPosition.Y != attackerY || secondLastHand.NextPosition.Y != victimY) return;

            game.Board.RemovePiece(secondLastHand.NextPosition);
        }
    }
}
