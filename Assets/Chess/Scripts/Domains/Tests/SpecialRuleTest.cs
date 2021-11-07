using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using NUnit.Framework;
using Zenject;

namespace Chess.Scripts.Domains.Tests
{
    public class SpecialRuleTest : ZenjectUnitTestFixture
    {
        [Inject] private PieceFactory _pieceFactory;
        [Inject] private MoveService _moveService;
        [Inject] private SpecialRuleService _specialRuleService;
        [Inject] private PromotionNotifier _promotionNotifier;

        private Player _whitePlayer;
        private Player _blackPlayer;
        private ISpecialRule[] _specialRules;

        [SetUp]
        public void Install()
        {
            Container.Bind<PieceFactory>().AsSingle();
            Container.Bind<MoveService>().AsSingle();
            Container.Bind<SpecialRuleService>().AsSingle();
            Container.Bind<PromotionNotifier>().AsSingle();

            _whitePlayer = new Player(PlayerColor.White);
            _blackPlayer = new Player(PlayerColor.Black);
            _specialRules = new ISpecialRule[]
            {
                new EnPassant(),
                new Castling(),
                new Promotion(_promotionNotifier),
            };

            Container.Inject(this);
        }
    }
}
