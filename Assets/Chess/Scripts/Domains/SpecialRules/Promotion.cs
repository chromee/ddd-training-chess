using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class Promotion : ISpecialRule
    {
        private readonly PromotionNotifier _notifier;

        public Promotion(PromotionNotifier notifier)
        {
            _notifier = notifier;
        }

        public void TryExecute(Board board)
        {
            var lastHand = board.LastPieceMovement;
            if (lastHand == null) return;

            var pawn = lastHand.MovedPiece;
            if (!pawn.IsType(PieceType.Pawn)) return;

            var y = pawn.Color == PlayerColor.White ? 7 : 0;
            if (lastHand.NextPosition.Y != y) return;

            _notifier.PawnPromotion(pawn);
        }
    }
}
