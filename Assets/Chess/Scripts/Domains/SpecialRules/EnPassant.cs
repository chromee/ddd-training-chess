using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class EnPassant : ISpecialRule
    {
        public void TryExecute(Board board)
        {
            var lastHand = board.LastPieceMovement;
            if (lastHand == null) return;

            var secondLastHand = board.SecondLastPieceMovement;
            if (secondLastHand == null || !secondLastHand.IsPawnTwoSpaceMove()) return;

            if (lastHand.NextPosition.X != secondLastHand.NextPosition.X) return;

            var attackerY = lastHand.MovedPiece.Color == PlayerColor.White ? 5 : 2;
            var victimY = lastHand.MovedPiece.Color == PlayerColor.White ? 4 : 3;
            if (lastHand.NextPosition.Y != attackerY || secondLastHand.NextPosition.Y != victimY) return;

            board.RemovePiece(secondLastHand.MovedPiece);
        }
    }
}
