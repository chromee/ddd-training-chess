using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;
using Zenject;

namespace Chess.Scripts.Domains.Tests
{
    public class BoardTest : ZenjectUnitTestFixture
    {
        [Inject] private PieceFactory _pieceFactory;

        private Player _whitePlayer;
        private Player _blackPlayer;

        [SetUp]
        public void Install()
        {
            Container.Bind<PieceFactory>().AsSingle();
            Container.Bind<MoveService>().AsSingle();

            _whitePlayer = new Player(PlayerColor.White);
            _blackPlayer = new Player(PlayerColor.Black);

            Container.Inject(this);
        }

        [Test]
        public void とりあえず白と黒のキングがいればボードが作れる()
        {
            var whitePieces = new[]
            {
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            Assert.DoesNotThrow(() => new Board(whitePieces.Concat(blackPieces).ToList()));
        }

        [Test]
        public void キングのいないボードは作れない()
        {
            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            Assert.Throws<NoKingException>(() => new Board(whitePieces.Concat(blackPieces).ToList()));
        }

        [Test]
        public void 複数キングのあるボードは作れない()
        {
            var whitePieces = new[]
            {
                _pieceFactory.CreateKing(_whitePlayer, new Position(2, 0)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            Assert.Throws<MultipleKingException>(() => new Board(whitePieces.Concat(blackPieces).ToList()));
        }

        [Test]
        public void チェックしてる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(4, 6)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsTrue(board.IsCheck(_whitePlayer));
        }

        [Test]
        public void チェックしてない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(4, 1)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsFalse(board.IsCheck(_whitePlayer));
        }

        [Test]
        public void 相手のコマをとれる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ ○ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(4, 5)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreatePawn(_blackPlayer, new Position(3, 6)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsTrue(board.CanPick(whitePieces[0]));
            Assert.IsTrue(board.CanPick(blackPieces[0]));
        }

        [Test]
        public void 相手のコマをとれない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ ○ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(4, 1)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreatePawn(_blackPlayer, new Position(3, 6)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsFalse(board.CanPick(whitePieces[0]));
            Assert.IsFalse(board.CanPick(blackPieces[0]));
        }

        [Test]
        public void とられないように回避できる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(4, 6)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsTrue(board.CanAvoid(blackPieces[0]));
        }

        [Test]
        public void とられないように回避できない()
        {
            // □ □ □ □ □ □ □ ☆
            // □ □ □ □ □ □ ● □
            // □ □ □ □ □ ● □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(1, 6)),
                _pieceFactory.CreatePawn(_whitePlayer, new Position(2, 5)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(0, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsFalse(board.CanAvoid(blackPieces[0]));
        }

        [Test]
        public void 指定したコマを守れる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ ○ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(3, 1)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateQueen(_blackPlayer, new Position(6, 6)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsTrue(board.CanProtect(blackPieces[1], new[] { blackPieces[0] }));
        }

        [Test]
        public void 指定したコマを守れない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ ○ ○ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(3, 1)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreatePawn(_blackPlayer, new Position(5, 6)),
                _pieceFactory.CreatePawn(_blackPlayer, new Position(6, 6)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsFalse(board.CanProtect(blackPieces[2], new[] { blackPieces[0], blackPieces[1], }));
        }
    }
}
