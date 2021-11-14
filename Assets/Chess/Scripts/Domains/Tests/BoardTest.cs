using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class BoardTest : DomainTestBase
    {
        [Test]
        public void とりあえず白と黒のキングがいればボードが作れる()
        {
            var whitePieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            Assert.DoesNotThrow(() => new Board(whitePieces.Concat(blackPieces).ToList()));
        }

        [Test]
        public void キングのいないボードは作れない()
        {
            var whitePieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            Assert.Throws<NoKingException>(() => new Board(whitePieces.Concat(blackPieces).ToList()));
        }

        [Test]
        public void 複数キングのあるボードは作れない()
        {
            var whitePieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.White, new Position(2, 0)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            Assert.Throws<MultipleKingException>(() => new Board(whitePieces.Concat(blackPieces).ToList()));
        }

        [Test]
        public void チェックしてる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.White, new Position(4, 6)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsTrue(board.IsCheck(PlayerColor.White));
        }

        [Test]
        public void チェックしてない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.White, new Position(4, 1)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsFalse(board.IsCheck(PlayerColor.White));
        }

        [Test]
        public void 相手のコマをとれる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ ○ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.White, new Position(4, 5)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.Black, new Position(3, 6)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsTrue(board.CanPick(whitePieces[0]));
            Assert.IsTrue(board.CanPick(blackPieces[0]));
        }

        [Test]
        public void 相手のコマをとれない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ ○ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.White, new Position(4, 1)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.Black, new Position(3, 6)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsFalse(board.CanPick(whitePieces[0]));
            Assert.IsFalse(board.CanPick(blackPieces[0]));
        }

        [Test]
        public void とられないように回避できる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ ● □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.White, new Position(4, 6)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsTrue(board.CanAvoid(blackPieces[0]));
        }

        [Test]
        public void とられないように回避できない()
        {
            // □ □ □ □ □ □ □ ☆
            // □ □ □ □ □ □ ● □
            // □ □ □ □ □ ● □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.White, new Position(1, 6)),
                PieceFactory.CreatePawn(PlayerColor.White, new Position(2, 5)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(0, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsFalse(board.CanAvoid(blackPieces[0]));
        }

        [Test]
        public void 指定したコマを守れる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ ○ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.White, new Position(3, 1)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.Black, new Position(6, 6)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsTrue(board.CanProtect(blackPieces[1], new[] { blackPieces[0] }));
        }

        [Test]
        public void 指定したコマを守れない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ ○ ○ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.White, new Position(3, 1)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.Black, new Position(5, 6)),
                PieceFactory.CreatePawn(PlayerColor.Black, new Position(6, 6)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            Assert.IsFalse(board.CanProtect(blackPieces[2], new[] { blackPieces[0], blackPieces[1], }));
        }
    }
}
