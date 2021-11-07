using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class KingTest : DomainTestBase
    {
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

            var whiteKing = PieceFactory.CreateKing(WhitePlayer, new Position(4, 0));
            var whitePieces = new[]
            {
                PieceFactory.CreateRook(WhitePlayer, new Position(0, 0)),
                PieceFactory.CreateRook(WhitePlayer, new Position(7, 0)),
                whiteKing,
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var destinations = whiteKing.MoveCandidates(board);
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

            var whiteKing = PieceFactory.CreateKing(WhitePlayer, new Position(4, 0));
            var whitePieces = new[]
            {
                PieceFactory.CreateRook(WhitePlayer, new Position(0, 0)),
                PieceFactory.CreateKnight(WhitePlayer, new Position(1, 0)),
                whiteKing,
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var destinations = whiteKing.MoveCandidates(board);
            Assert.IsFalse(destinations.Contains(new Position(2, 0)));
        }

        [Test]
        public void 動くとチェックになる状況だとキャスリングできない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ ○ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // ● □ □ □ ★ □ □ □

            var whiteKing = PieceFactory.CreateKing(WhitePlayer, new Position(4, 0));
            var whitePieces = new[]
            {
                PieceFactory.CreateRook(WhitePlayer, new Position(0, 0)),
                whiteKing,
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateRook(BlackPlayer, new Position(2, 3)),
                PieceFactory.CreateKing(BlackPlayer, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var destinations = whiteKing.MoveCandidates(board);
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

            var whiteRook = PieceFactory.CreateRook(WhitePlayer, new Position(0, 0));
            var whiteKing = PieceFactory.CreateKing(WhitePlayer, new Position(4, 0));
            var whitePieces = new[] { whiteRook, whiteKing, };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            MoveService.Move(whiteKing, new Position(2, 0), game);

            Assert.AreEqual(new Position(3, 0), whiteRook.Position);
        }
    }
}
