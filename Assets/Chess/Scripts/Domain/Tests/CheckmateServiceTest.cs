using System.Linq;
using Chess.Domain.Boards;
using Chess.Domain.Games;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;
using NUnit.Framework;
using Zenject;

namespace Chess.Scripts.Domain.Tests
{
    public class CheckmateServiceTest : ZenjectUnitTestFixture
    {
        [Inject] private PieceFactory _pieceFactory;
        [Inject] private PieceService _pieceService;
        [Inject] private CheckService _checkService;
        [Inject] private MoveService _moveService;
        [Inject] private CheckmateService _checkmateService;

        private Player _whitePlayer;
        private Player _blackPlayer;

        [SetUp]
        public void Install()
        {
            Container.Bind<PieceFactory>().AsSingle();
            Container.Bind<PieceService>().AsSingle();
            Container.Bind<CheckService>().AsSingle();
            Container.Bind<MoveService>().AsSingle();
            Container.Bind<CheckmateService>().AsSingle();

            _whitePlayer = new Player(PlayerColor.White);
            _blackPlayer = new Player(PlayerColor.Black);

            Container.Inject(this);
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
            var game = new Game(board, _whitePlayer, _blackPlayer, _moveService);

            var isCheckmate = _checkmateService.IsCheckmate(game.Board, game.CurrentTurnPlayer, game.NextTurnPlayer);

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
            var game = new Game(board, _whitePlayer, _blackPlayer, _moveService);

            game.MovePiece(whitePieces[0], new Position(3, 4));

            var isCheckmate = _checkmateService.IsCheckmate(game.Board, game.CurrentTurnPlayer, game.NextTurnPlayer);

            Assert.IsFalse(isCheckmate);
        }

        [Test]
        public void チェックコマ殺害によるチェックメイト回避()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ ○ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ ●
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □
            //         ↓
            // □ □ □ □ ☆ □ □ □
            // □ □ □ ○ □ □ □ □
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
                _pieceFactory.CreateQueen(_blackPlayer, new Position(4, 6)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _moveService);

            game.MovePiece(whitePieces[0], new Position(3, 4));

            var isCheckmate = _checkmateService.IsCheckmate(game.Board, game.CurrentTurnPlayer, game.NextTurnPlayer);

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
            var game = new Game(board, _whitePlayer, _blackPlayer, _moveService);

            game.MovePiece(whitePieces[0], new Position(3, 3));

            var isCheckmate = _checkmateService.IsCheckmate(game.Board, game.CurrentTurnPlayer, game.NextTurnPlayer);

            Assert.IsFalse(isCheckmate);
        }
    }
}
