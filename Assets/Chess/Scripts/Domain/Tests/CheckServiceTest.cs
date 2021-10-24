using System.Linq;
using Chess.Domain;
using Chess.Domain.Boards;
using Chess.Domain.Games;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;
using NUnit.Framework;
using Zenject;

namespace Chess.Scripts.Domain.Tests
{
    public class CheckServiceTest : ZenjectUnitTestFixture
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
            var game = new Game(board, _whitePlayer, _blackPlayer, _moveService);

            var isCheck = _checkService.IsCheck(game.Board, game.CurrentTurnPlayer, game.NextTurnPlayer);

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
            var game = new Game(board, _whitePlayer, _blackPlayer, _moveService);

            var isCheck = _checkService.IsCheck(game.Board, game.CurrentTurnPlayer, game.NextTurnPlayer);

            Assert.IsFalse(isCheck);
        }

    }
}
