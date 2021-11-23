using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.SpecialRules;

namespace Chess.Scripts.Applications.SpecialRules
{
    public class PromotionPresenterFactory
    {
        private readonly PromotionNotifier _promotionNotifier;
        private readonly PromotionExecutor _promotionExecutor;
        private readonly IPromotionView _promotionView;

        public PromotionPresenterFactory(PromotionNotifier promotionNotifier, PromotionExecutor promotionExecutor, IPromotionView promotionView)
        {
            _promotionNotifier = promotionNotifier;
            _promotionExecutor = promotionExecutor;
            _promotionView = promotionView;
        }

        public PromotionPresenter Create(Board board) => new PromotionPresenter(_promotionNotifier, _promotionExecutor, _promotionView, board);
    }
}
