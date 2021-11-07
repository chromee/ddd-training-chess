using System;
using Chess.Scripts.Applications.Games;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using UniRx;
using VContainer.Unity;

namespace Chess.Scripts.Applications.SpecialRules
{
    public class PromotionPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        private readonly PromotionNotifier _promotionNotifier;
        private readonly SpecialRuleService _specialRuleService;
        private readonly GameRegistry _gameRegistry;
        private readonly IPromotionView _promotionView;

        public PromotionPresenter(
            PromotionNotifier promotionNotifier,
            SpecialRuleService specialRuleService,
            GameRegistry gameRegistry,
            IPromotionView promotionView
        )
        {
            _promotionNotifier = promotionNotifier;
            _specialRuleService = specialRuleService;
            _gameRegistry = gameRegistry;
            _promotionView = promotionView;
        }

        public void Initialize()
        {
            _promotionNotifier.OnPawnPromotion.Subscribe(piece =>
            {
                if (piece == null) return;
                _promotionView.ShowPromotionDialogue();
            }).AddTo(_disposable);

            var board = _gameRegistry.CurrentGame.Board;

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
