using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.SpecialRules;

namespace Chess.Scripts.Applications.SpecialRules
{
    public class PromotionPresenterFactory
    {
        private readonly PromotionNotifier _promotionNotifier;
        private readonly SpecialRuleService _specialRuleService;
        private readonly IPromotionView _promotionView;

        public PromotionPresenterFactory(PromotionNotifier promotionNotifier, SpecialRuleService specialRuleService, IPromotionView promotionView)
        {
            _promotionNotifier = promotionNotifier;
            _specialRuleService = specialRuleService;
            _promotionView = promotionView;
        }

        public PromotionPresenter Create(Board board) => new PromotionPresenter(_promotionNotifier, _specialRuleService, _promotionView, board);
    }
}
