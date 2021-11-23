using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class DomainTestBase
    {
        protected PieceFactory PieceFactory;
        protected PromotionExecutor PromotionExecutor;
        protected PromotionNotifier PromotionNotifier;
        protected MoveService MoveService;

        [SetUp]
        public void Install()
        {
            PieceFactory = new PieceFactory();

            PromotionNotifier = new PromotionNotifier();
            PromotionExecutor = new PromotionExecutor(PromotionNotifier, PieceFactory);
            var specialRuleExecutorFactory = new SpecialRuleExecutorFactory(PromotionNotifier);

            MoveService = new MoveService(specialRuleExecutorFactory);
        }
    }
}
