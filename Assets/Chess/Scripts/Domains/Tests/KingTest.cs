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
    public class KingTest : ZenjectUnitTestFixture
    {
        [Inject] private PieceFactory _pieceFactory;
        [Inject] private MoveService _moveService;

        private Player _whitePlayer;
        private Player _blackPlayer;
        private SpecialRule[] _specialRules;

        [SetUp]
        public void Install()
        {
            Container.Bind<PieceFactory>().AsSingle();
            Container.Bind<MoveService>().AsSingle();

            _whitePlayer = new Player(PlayerColor.White);
            _blackPlayer = new Player(PlayerColor.Black);
            _specialRules = new SpecialRule[]
            {
                new EnPassant(),
                new Castling(),
                new Promotion(),
            };

            Container.Inject(this);
        }

        [Test]
        public void キャスリングできる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // ● □ □ □ ★ □ □ ●

            var whitePieces = new[]
            {
                _pieceFactory.CreateRook(_whitePlayer, new Position(0, 0)),
                _pieceFactory.CreateRook(_whitePlayer, new Position(7, 0)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(4, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var destinations = whitePieces[2].MoveCandidates(board);
            Assert.IsTrue(destinations.Contains(new Position(2, 0)));
            Assert.IsTrue(destinations.Contains(new Position(6, 0)));
        }

        [Test]
        public void 間にコマがあるとキャスリングできない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // ● ● □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateRook(_whitePlayer, new Position(0, 0)),
                _pieceFactory.CreateKnight(_whitePlayer, new Position(1, 0)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(4, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var destinations = whitePieces[2].MoveCandidates(board);
            Assert.IsFalse(destinations.Contains(new Position(2, 0)));
        }

        [Test]
        public void 動くとチェックになるとキャスリングできない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ ○ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // ● □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateRook(_whitePlayer, new Position(0, 0)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(4, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateRook(_blackPlayer, new Position(2, 3)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var destinations = whitePieces[1].MoveCandidates(board);
            Assert.IsFalse(destinations.Contains(new Position(2, 0)));
        }

        [Test]
        public void キャスリングしたらルークが動く()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // ● □ □ □ ★ □ □ □
            //         ↓
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ ★ ● □ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateRook(_whitePlayer, new Position(0, 0)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(4, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            _moveService.Move(whitePieces[1], new Position(2, 0), game);

            Assert.AreEqual(new Position(3, 0), whitePieces[0].Position);
        }
    }
}
