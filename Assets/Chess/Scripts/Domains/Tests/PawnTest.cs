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
    public class PawnTest : ZenjectUnitTestFixture
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
        public void ポーンが所定位置にいるときは2マス移動できる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ ○ □ □ □
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
                _pieceFactory.CreatePawn(_blackPlayer, new Position(3, 6)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var wDestinations = whitePieces[0].MoveCandidates(board);
            var wCorrectDestinations = new[] { new Position(3, 2), new Position(3, 3), };
            Assert.That(wCorrectDestinations, Is.EquivalentTo(wDestinations));

            var bDestinations = blackPieces[0].MoveCandidates(board);
            var bCorrectDestinations = new[] { new Position(3, 5), new Position(3, 4), };
            Assert.That(bCorrectDestinations, Is.EquivalentTo(bDestinations));
        }

        [Test]
        public void ポーンが所定位置にいてもブロックするコマがあるときは2マス移動できない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ○ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(3, 1)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreatePawn(_blackPlayer, new Position(3, 2)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var wDestinations = whitePieces[0].MoveCandidates(board);
            Assert.AreEqual(0, wDestinations.Length);
        }

        [Test]
        public void ポーンが斜めのコマをとれる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ○ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(3, 2)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreatePawn(_blackPlayer, new Position(4, 3)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var destinations = whitePieces[0].MoveCandidates(board);
            var correctDestinations = new[] { new Position(3, 3), new Position(4, 3), };
            Assert.That(correctDestinations, Is.EquivalentTo(destinations));
        }

        [Test]
        public void ポーンがアンパッサンできる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ○ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □
            //          ↓
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ○ ● □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □
            //          ↓
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ○ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(_whitePlayer, new Position(3, 1)),
                _pieceFactory.CreateKing(_whitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreatePawn(_blackPlayer, new Position(4, 3)),
                _pieceFactory.CreateKing(_blackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, _whitePlayer, _blackPlayer, _specialRules);
            _moveService.Move(whitePieces[0], new Position(3, 3), game);

            var destinations = blackPieces[0].MoveCandidates(board);
            var correctDestinations = new[] { new Position(3, 2), new Position(4, 2), };
            Assert.That(correctDestinations, Is.EquivalentTo(destinations));

            _moveService.Move(blackPieces[0], new Position(3, 2), game);
            Assert.IsTrue(whitePieces[0].IsDead);
        }
    }
}
