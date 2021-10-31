using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class EnPassant : SpecialRule
    {
        public override void TryExecute(Board board)
        {
            var lastHand = board.LastPieceMovement;
            var secondLastHand = board.SecondLastPieceMovement;
            if (secondLastHand == null || !secondLastHand.IsPawnTwoSpaceMove()) return;
            if (lastHand == null) return;
            if (lastHand.NextPosition.X != secondLastHand.NextPosition.X) return;
            var yDiff = lastHand.MovedPiece.Color == PlayerColor.White ? -1 : 1;
            if (lastHand.NextPosition.Y - secondLastHand.NextPosition.Y == yDiff) return;
            secondLastHand.MovedPiece.Die();
        }
    }
}
