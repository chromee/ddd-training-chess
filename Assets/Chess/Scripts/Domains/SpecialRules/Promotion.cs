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

        public void TryExecute(Game game)
        {
            if (game.Logger.LastPieceMovement == null) return;
            var lastHand = game.Logger.LastPieceMovement.Value;

            if (lastHand.MovedPieceType != PieceType.Pawn) return;

            var y = lastHand.MovedPieceColor == PlayerColor.White ? 7 : 0;
            if (lastHand.NextPosition.Y != y) return;

            _notifier.PawnPromotion(lastHand.NextPosition);
        }
    }
}
