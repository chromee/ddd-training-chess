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
        protected GameService GameService;
        protected SpecialRuleService SpecialRuleService;
        protected PromotionNotifier PromotionNotifier;

        protected ISpecialRule[] SpecialRules;

        [SetUp]
        public void Install()
        {
            PieceFactory = new PieceFactory();
            MoveService = new MoveService();
            GameService = new GameService();
            PromotionNotifier = new PromotionNotifier();
            SpecialRuleService = new SpecialRuleService(PromotionNotifier, PieceFactory);

            SpecialRules = new ISpecialRule[]
            {
                new EnPassant(),
                new Castling(),
                new Promotion(PromotionNotifier),
            };
        }
    }
}
