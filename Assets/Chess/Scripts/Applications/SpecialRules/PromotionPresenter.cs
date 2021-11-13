using System;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.SpecialRules;
using UniRx;

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
            promotionNotifier.OnPawnPromotion.Subscribe(piece =>
            {
                if (piece == null) return;
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
