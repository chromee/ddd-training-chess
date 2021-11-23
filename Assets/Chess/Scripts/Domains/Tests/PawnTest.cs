using System.Linq;
using Chess.Scripts.Domains.Boards;
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

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board);

            var wDestinations = whitePawn.MoveCandidates(game);
            var wCorrectDestinations = new[] { new Position(3, 2), new Position(3, 3), };
            Assert.That(wDestinations, Is.EquivalentTo(wCorrectDestinations));

            var bDestinations = blackPawn.MoveCandidates(game);
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

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board);

            var wDestinations = whitePawn.MoveCandidates(game);
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

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board);

            var destinations = whitePawn.MoveCandidates(game);
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

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board);
            MoveService.Move(game, whitePawn, new Position(3, 3));

            var destinations = blackPawn.MoveCandidates(game);
            var correctDestinations = new[] { new Position(3, 2), new Position(4, 2), };
            Assert.That(destinations, Is.EquivalentTo(correctDestinations));

            MoveService.Move(game, blackPawn, new Position(3, 2));
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

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board);

            MoveService.Move(game, whitePawn, new Position(1, 7));

            Assert.AreEqual(whitePawn.Position, PromotionNotifier.TargetPawnPosition);

            PromotionExecutor.Promotion(board, PieceType.Queen);

            Assert.IsTrue(whitePawn.IsDead);
            Assert.IsTrue(board.GetPiece(new Position(1, 7)).IsType(PieceType.Queen));
        }
    }
}
