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

        private readonly PromotionNotifier _promotionNotifier;
        private readonly SpecialRuleService _specialRuleService;
        private readonly IPromotionView _promotionView;

        public PromotionPresenter(
            PromotionNotifier promotionNotifier,
            SpecialRuleService specialRuleService,
            IPromotionView promotionView
        )
        {
            _promotionNotifier = promotionNotifier;
            _specialRuleService = specialRuleService;
            _promotionView = promotionView;
        }

        public void Bind(Board board)
        {
            _promotionNotifier.OnPawnPromotion.Subscribe(piece =>
            {
                if (piece == null) return;
                _promotionView.ShowPromotionDialogue();
            }).AddTo(_disposable);

            _promotionView.OnSelectPieceType.Subscribe(type =>
            {
                _specialRuleService.Promotion(board, type.ToDomain());
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
