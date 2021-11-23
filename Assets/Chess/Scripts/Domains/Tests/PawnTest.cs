using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class PawnTest : DomainTestBase
    {
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

            var whitePawn = PieceFactory.CreatePawn(PlayerColor.White, new Position(3, 1));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPawn = PieceFactory.CreatePawn(PlayerColor.Black, new Position(3, 6));
            var blackPieces = new[]
            {
                blackPawn,
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var wDestinations = game.PieceMovementSolver.MoveCandidates(whitePawn);
            var wCorrectDestinations = new[] { new Position(3, 2), new Position(3, 3), };
            Assert.That(wDestinations, Is.EquivalentTo(wCorrectDestinations));

            var bDestinations = game.PieceMovementSolver.MoveCandidates(blackPawn);
            var bCorrectDestinations = new[] { new Position(3, 5), new Position(3, 4), };
            Assert.That(bDestinations, Is.EquivalentTo(bCorrectDestinations));
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

            var whitePawn = PieceFactory.CreatePawn(PlayerColor.White, new Position(3, 1));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.Black, new Position(3, 2)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var wDestinations = game.PieceMovementSolver.MoveCandidates(whitePawn);
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

            var whitePawn = PieceFactory.CreatePawn(PlayerColor.White, new Position(3, 2));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.Black, new Position(4, 3)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var destinations = game.PieceMovementSolver.MoveCandidates(whitePawn);
            var correctDestinations = new[] { new Position(3, 3), new Position(4, 3), };
            Assert.That(destinations, Is.EquivalentTo(correctDestinations));
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

            var whitePawn = PieceFactory.CreatePawn(PlayerColor.White, new Position(3, 1));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPawn = PieceFactory.CreatePawn(PlayerColor.Black, new Position(4, 3));
            var blackPieces = new[]
            {
                blackPawn,
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            PieceMovementExecutor.Move(game, whitePawn, new Position(3, 3));

            var destinations = game.PieceMovementSolver.MoveCandidates(blackPawn);
            var correctDestinations = new[] { new Position(3, 2), new Position(4, 2), };
            Assert.That(destinations, Is.EquivalentTo(correctDestinations));

            PieceMovementExecutor.Move(game, blackPawn, new Position(3, 2));
            Assert.IsTrue(whitePawn.IsDead);
        }

        [Test]
        public void プロモーション()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ ● □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □
            //          ↓
            // □ □ □ □ ☆ □ ● □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePawn = PieceFactory.CreatePawn(PlayerColor.White, new Position(1, 6));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 1)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            PieceMovementExecutor.Move(game, whitePawn, new Position(1, 7));

            Assert.AreEqual(whitePawn.Position, PromotionNotifier.TargetPawnPosition);

            PromotionExecutor.Promotion(game.Board, PieceType.Queen);

            Assert.IsTrue(whitePawn.IsDead);
            Assert.IsTrue(game.Board.GetPiece(new Position(1, 7)).IsType(PieceType.Queen));
        }
    }
}
