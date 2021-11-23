using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class DomainTestBase
    {
        protected PieceFactory PieceFactory;
        protected MoveService MoveService;
        protected PromotionExecutor PromotionExecutor;
        protected PromotionNotifier PromotionNotifier;

        [SetUp]
        public void Install()
        {
            PieceFactory = new PieceFactory();

            PromotionNotifier = new PromotionNotifier();
            PromotionExecutor = new PromotionExecutor(PromotionNotifier, PieceFactory);
            var specialRuleExecutorFactory = new SpecialRuleExecutorFactory(PromotionNotifier);
            var specialRuleExecutor = specialRuleExecutorFactory.Create();

            MoveService = new MoveService(specialRuleExecutor);
        }
    }
}
