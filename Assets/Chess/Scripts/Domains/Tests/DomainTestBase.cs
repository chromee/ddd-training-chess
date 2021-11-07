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
        protected SpecialRuleService SpecialRuleService;
        protected PromotionNotifier PromotionNotifier;

        protected Player WhitePlayer;
        protected Player BlackPlayer;
        protected ISpecialRule[] SpecialRules;

        [SetUp]
        public void Install()
        {
            PieceFactory = new PieceFactory();
            MoveService = new MoveService();
            PromotionNotifier = new PromotionNotifier();
            SpecialRuleService = new SpecialRuleService(PromotionNotifier, PieceFactory);

            WhitePlayer = new Player(PlayerColor.White);
            BlackPlayer = new Player(PlayerColor.Black);

            SpecialRules = new ISpecialRule[]
            {
                new EnPassant(),
                new Castling(),
                new Promotion(PromotionNotifier),
            };
        }
    }
}
