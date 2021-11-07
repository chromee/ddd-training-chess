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

            var whitePawn = PieceFactory.CreatePawn(WhitePlayer, new Position(3, 1));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPawn = PieceFactory.CreatePawn(BlackPlayer, new Position(3, 6));
            var blackPieces = new[]
            {
                blackPawn,
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var wDestinations = whitePawn.MoveCandidates(board);
            var wCorrectDestinations = new[] { new Position(3, 2), new Position(3, 3), };
            Assert.That(wCorrectDestinations, Is.EquivalentTo(wDestinations));

            var bDestinations = blackPawn.MoveCandidates(board);
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

            var whitePawn = PieceFactory.CreatePawn(WhitePlayer, new Position(3, 1));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(BlackPlayer, new Position(3, 2)),
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var wDestinations = whitePawn.MoveCandidates(board);
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

            var whitePawn = PieceFactory.CreatePawn(WhitePlayer, new Position(3, 2));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(BlackPlayer, new Position(4, 3)),
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var destinations = whitePawn.MoveCandidates(board);
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

            var whitePawn = PieceFactory.CreatePawn(WhitePlayer, new Position(3, 1));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPawn = PieceFactory.CreatePawn(BlackPlayer, new Position(4, 3));
            var blackPieces = new[]
            {
                blackPawn,
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);
            MoveService.Move(whitePawn, new Position(3, 3), game);

            var destinations = blackPawn.MoveCandidates(board);
            var correctDestinations = new[] { new Position(3, 2), new Position(4, 2), };
            Assert.That(correctDestinations, Is.EquivalentTo(destinations));

            MoveService.Move(blackPawn, new Position(3, 2), game);
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

            var whitePawn = PieceFactory.CreatePawn(WhitePlayer, new Position(1, 6));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 1)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            MoveService.Move(whitePawn, new Position(1, 7), game);

            Assert.AreEqual(whitePawn, PromotionNotifier.TargetPawn);

            SpecialRuleService.Promotion(board, PieceType.Queen);

            Assert.IsTrue(whitePawn.IsDead);
            Assert.IsTrue(board.GetPiece(new Position(1, 7)).IsType(PieceType.Queen));
        }
    }
}
