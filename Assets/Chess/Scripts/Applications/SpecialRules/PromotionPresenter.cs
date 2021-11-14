using System;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.SpecialRules;
using UniRx;
using PieceType = Chess.Scripts.Domains.Pieces.PieceType;

namespace Chess.Scripts.Applications.SpecialRules
{
    public class PromotionPresenter : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        public PromotionPresenter(
            PromotionNotifier promotionNotifier,
            SpecialRuleService specialRuleService,
            IPromotionView promotionView,
            Board board
        )
        {
            promotionNotifier.OnPawnPromotion.Subscribe(position =>
            {
                var piece = board.GetPiece(position);
                if (piece == null || !piece.IsType(PieceType.Pawn)) return;
                promotionView.ShowPromotionDialogue();
            }).AddTo(_disposable);

            promotionView.OnSelectPieceType.Subscribe(type =>
            {
                specialRuleService.Promotion(board, type.ToDomain());
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
