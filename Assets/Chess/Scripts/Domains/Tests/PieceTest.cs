using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class PieceTest : DomainTestBase
    {
        [Test]
        public void 直進できるコマが端まで移動できる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □
            // □ □ □ □ □ □ □ ●

            var whiteRook = PieceFactory.CreateRook(PlayerColor.White, new Position(0, 0));
            var whitePieces = new[]
            {
                whiteRook,
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 1)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var destinations = game.PieceMovementSolver.MoveCandidates(whiteRook);
            var correctDestinations = new[]
            {
                new Position(0, 1), new Position(0, 2), new Position(0, 3), new Position(0, 4),
                new Position(0, 5), new Position(0, 6), new Position(0, 7),
                new Position(1, 0), new Position(2, 0), new Position(3, 0), new Position(4, 0),
                new Position(5, 0), new Position(6, 0), new Position(7, 0),
            };
            Assert.That(correctDestinations, Is.EquivalentTo(destinations));
        }

        [Test]
        public void 直進できるコマが他のコマにブロックされる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ ●
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ ●

            var whiteRook = PieceFactory.CreateRook(PlayerColor.White, new Position(0, 0));
            var whitePieces = new[]
            {
                whiteRook,
                PieceFactory.CreatePawn(PlayerColor.White, new Position(0, 5)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var destinations = game.PieceMovementSolver.MoveCandidates(whiteRook);
            var correctDestinations = new[]
            {
                new Position(0, 1), new Position(0, 2), new Position(0, 3), new Position(0, 4),
                new Position(1, 0), new Position(2, 0),
            };
            Assert.That(correctDestinations, Is.EquivalentTo(destinations));
        }
    }
}
