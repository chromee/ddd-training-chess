using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class GameTest : DomainTestBase
    {
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
                PieceFactory.CreateQueen(PlayerColor.White, new Position(3, 3)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, SpecialRules);

            var isCheck = GameService.IsCheck(game, game.CurrentTurnPlayer);

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
                PieceFactory.CreateQueen(PlayerColor.White, new Position(3, 3)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.Black, new Position(3, 4)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, SpecialRules);

            var isCheck = GameService.IsCheck(game, game.CurrentTurnPlayer);

            Assert.IsFalse(isCheck);
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
                PieceFactory.CreateQueen(PlayerColor.White, new Position(3, 6)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 5)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, SpecialRules);

            var isCheckmate = GameService.IsCheck(game, game.CurrentTurnPlayer);

            Assert.IsTrue(isCheckmate);
        }

        [Test]
        public void キングの移動によるチェックメイト回避が可能()
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
                PieceFactory.CreateQueen(PlayerColor.White, new Position(0, 4)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, SpecialRules);

            MoveService.Move(game, whitePieces[0], new Position(3, 4));

            var isCheckmate = GameService.IsCheck(game, game.CurrentTurnPlayer);

            Assert.IsFalse(isCheckmate);
        }

        [Test]
        public void チェックコマ殺害によるチェックメイト回避が可能()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ ●
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ ○ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □
            //         ↓
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ ○ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.White, new Position(0, 4)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.Black, new Position(6, 1)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, SpecialRules);

            MoveService.Move(game, whitePieces[0], new Position(3, 4));

            var isCheckmate = GameService.IsCheck(game, game.CurrentTurnPlayer);

            Assert.IsFalse(isCheckmate);
        }

        [Test]
        public void ブロックによるチェックメイト回避が可能()
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
                PieceFactory.CreateQueen(PlayerColor.White, new Position(0, 3)),
                PieceFactory.CreateQueen(PlayerColor.White, new Position(7, 4)),
                PieceFactory.CreateQueen(PlayerColor.White, new Position(2, 1)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.Black, new Position(4, 6)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, SpecialRules);

            MoveService.Move(game, whitePieces[0], new Position(3, 3));

            var isCheckmate = GameService.IsCheck(game, game.CurrentTurnPlayer);

            Assert.IsFalse(isCheckmate);
        }

        [Test]
        public void スティルメイト()
        {
            // □ □ □ □ □ □ □ ☆
            // □ □ □ □ □ □ □ ○
            // □ □ □ □ □ □ □ ●
            // □ □ □ □ □ ● □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ ★
            //         ↓
            // □ □ □ □ □ □ □ ☆
            // □ □ □ □ □ □ □ ○
            // □ □ □ □ □ □ □ ●
            // □ □ □ □ □ □ ● □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ ★

            var whitePieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.White, new Position(7, 5)),
                PieceFactory.CreateRook(PlayerColor.White, new Position(5, 4)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(7, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(PlayerColor.Black, new Position(7, 6)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(7, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, SpecialRules);

            MoveService.Move(game, whitePieces[1], new Position(6, 4));

            Assert.AreEqual(GameStatus.Stalemate, game.GameStatus.Value);
        }
    }
}
