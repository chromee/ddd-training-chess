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
    public class GameTest : ZenjectUnitTestFixture
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
        public void チェック()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(3, 3)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            var isCheck = game.IsCheck();

            Assert.IsTrue(isCheck);
        }

        [Test]
        public void ブロックによるチェック回避()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ○ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(3, 3)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateQueen(_blackPlayer, new Position(3, 4)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            var isCheck = game.IsCheck();

            Assert.IsFalse(isCheck);
        }

        [Test]
        public void チェックメイト()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(3, 6)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 5)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            var isCheckmate = game.IsCheckmate();

            Assert.IsTrue(isCheckmate);
        }

        [Test]
        public void キングの移動によるチェックメイト回避()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ ●
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □
            //         ↓
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(0, 4)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            _moveService.Move(whitePieces[0], new Position(3, 4), game);

            var isCheckmate = game.IsCheckmate();

            Assert.IsFalse(isCheckmate);
        }

        [Test]
        public void チェックコマ殺害によるチェックメイト回避()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ ●
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ ○ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □
            //         ↓
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ ○ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(0, 4)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateQueen(_blackPlayer, new Position(6, 1)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            _moveService.Move(whitePieces[0], new Position(3, 4), game);

            var isCheckmate = game.IsCheckmate();

            Assert.IsFalse(isCheckmate);
        }

        [Test]
        public void ブロックによるチェックメイト回避()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ ○ □ □ □ □
            // □ □ □ □ □ □ □ □
            // ● □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ ●
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ ● □ □
            // □ □ □ □ ★ □ □ □
            //         ↓
            // □ □ □ □ ☆ □ □ □
            // □ □ □ ○ □ □ □ □
            // □ □ □ □ □ □ □ □
            // ● □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ ● □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(_whitePlayer, new Position(0, 3)),
                _pieceFactory.CreateQueen(_whitePlayer, new Position(7, 4)),
                _pieceFactory.CreateQueen(_whitePlayer, new Position(2, 1)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateQueen(_blackPlayer, new Position(4, 6)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            _moveService.Move(whitePieces[0], new Position(3, 3), game);

            var isCheckmate = game.IsCheckmate();

            Assert.IsFalse(isCheckmate);
        }
    }
}
