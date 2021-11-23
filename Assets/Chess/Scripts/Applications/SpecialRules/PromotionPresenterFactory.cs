using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.SpecialRules;

namespace Chess.Scripts.Applications.SpecialRules
{
    public class PromotionPresenterFactory
    {
        private readonly PromotionExecutor _promotionExecutor;
        private readonly PromotionNotifier _promotionNotifier;
        private readonly IPromotionView _promotionView;

        public PromotionPresenterFactory(PromotionNotifier promotionNotifier, PromotionExecutor promotionExecutor, IPromotionView promotionView)
        {
            _promotionNotifier = promotionNotifier;
            _promotionExecutor = promotionExecutor;
            _promotionView = promotionView;
        }

        public PromotionPresenter Create(Board board) => new(_promotionNotifier, _promotionExecutor, _promotionView, board);
    }
}
