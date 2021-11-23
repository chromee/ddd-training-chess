using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class DomainTestBase
    {
        protected GameFactory GameFactory;
        protected PieceFactory PieceFactory;
        protected PieceMovementExecutor PieceMovementExecutor;
        protected PromotionExecutor PromotionExecutor;
        protected PromotionNotifier PromotionNotifier;

        [SetUp]
        public void Install()
        {
            PieceFactory = new PieceFactory();

            PromotionNotifier = new PromotionNotifier();
            PromotionExecutor = new PromotionExecutor(PromotionNotifier, PieceFactory);
            var specialRuleExecutorFactory = new SpecialRuleExecutorFactory(PromotionNotifier);

            GameFactory = new GameFactory(PieceFactory, specialRuleExecutorFactory);

            PieceMovementExecutor = new PieceMovementExecutor();
        }
    }
}
