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
    public class MoveServiceTest : ZenjectUnitTestFixture
    {
        [Inject] private PieceFactory _pieceFactory;
        [Inject] private MoveService _moveService;

        private Player _whitePlayer;
        private Player _blackPlayer;
        private ISpecialRule[] _specialRules;

        [SetUp]
        public void Install()
        {
            Container.Bind<PieceFactory>().AsSingle();
            Container.Bind<MoveService>().AsSingle();

            _whitePlayer = new Player(PlayerColor.White);
            _blackPlayer = new Player(PlayerColor.Black);
            _specialRules = new ISpecialRule[]
            {
                new EnPassant(),
                new Castling(),
                new Promotion(new PromotionNotifier()),
            };

            Container.Inject(this);
        }

        [Test]
        public void コマは2回連続動かせない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
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

            _moveService.Move(whitePieces[0], new Position(3, 4), game);

            Assert.Throws<WrongPlayerException>(() =>
                _moveService.Move(whitePieces[0], new Position(3, 3), game));
        }

        [Test]
        public void とられたコマは動かせない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
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

            _moveService.Move(whitePieces[0], new Position(3, 4), game);

            Assert.Throws<PieceNotExistOnBoardException>(() =>
                _moveService.Move(blackPieces[0], new Position(3, 5), game));
        }

        [Test]
        public void コマの行動範囲外には移動できない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            Assert.Throws<OutOfRangePieceMovableRangeException>(() =>
                _moveService.Move(whitePieces[0], new Position(3, 2), game));
        }

        [Test]
        public void 味方コマがいる位置には移動できない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(3, 1)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            Assert.Throws<OutOfRangePieceMovableRangeException>(() =>
                _moveService.Move(whitePieces[1], new Position(3, 1), game));
        }

        [Test]
        public void 自殺行動はできない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ○ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreatePawn(_blackPlayer, new Position(4, 2)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);

            Assert.Throws<SuicideMoveException>(() =>
                _moveService.Move(whitePieces[0], new Position(3, 1), game));
        }
    }
}
