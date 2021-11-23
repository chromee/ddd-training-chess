using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
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

            var whiteKing = PieceFactory.CreateKing(PlayerColor.White, new Position(4, 0));
            var whitePieces = new[]
            {
                PieceFactory.CreateRook(PlayerColor.White, new Position(0, 0)),
                PieceFactory.CreateRook(PlayerColor.White, new Position(7, 0)),
                whiteKing,
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var destinations = game.PieceMovementSolver.MoveCandidates(whiteKing);
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

            var whiteKing = PieceFactory.CreateKing(PlayerColor.White, new Position(4, 0));
            var whitePieces = new[]
            {
                PieceFactory.CreateRook(PlayerColor.White, new Position(0, 0)),
                PieceFactory.CreateKnight(PlayerColor.White, new Position(1, 0)),
                whiteKing,
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var destinations = game.PieceMovementSolver.MoveCandidates(whiteKing);
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

            var whiteKing = PieceFactory.CreateKing(PlayerColor.White, new Position(4, 0));
            var whitePieces = new[]
            {
                PieceFactory.CreateRook(PlayerColor.White, new Position(0, 0)),
                whiteKing,
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateRook(PlayerColor.Black, new Position(2, 3)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var destinations = game.PieceMovementSolver.MoveCandidates(whiteKing);
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

            var whiteRook = PieceFactory.CreateRook(PlayerColor.White, new Position(0, 0));
            var whiteKing = PieceFactory.CreateKing(PlayerColor.White, new Position(4, 0));
            var whitePieces = new[] { whiteRook, whiteKing, };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            PieceMovementExecutor.Move(game, whiteKing, new Position(2, 0));

            Assert.AreEqual(new Position(3, 0), whiteRook.Position);
        }
    }
}
