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
                PieceFactory.CreateQueen(WhitePlayer, new Position(3, 3)),
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            var isCheck = game.IsCheck();

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
                PieceFactory.CreateQueen(WhitePlayer, new Position(3, 3)),
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateQueen(BlackPlayer, new Position(3, 4)),
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            var isCheck = game.IsCheck();

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
                PieceFactory.CreateQueen(WhitePlayer, new Position(3, 6)),
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 5)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            var isCheckmate = game.IsCheckmate();

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
                PieceFactory.CreateQueen(WhitePlayer, new Position(0, 4)),
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            MoveService.Move(whitePieces[0], new Position(3, 4), game);

            var isCheckmate = game.IsCheckmate();

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
                PieceFactory.CreateQueen(WhitePlayer, new Position(0, 4)),
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateQueen(BlackPlayer, new Position(6, 1)),
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            MoveService.Move(whitePieces[0], new Position(3, 4), game);

            var isCheckmate = game.IsCheckmate();

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
                PieceFactory.CreateQueen(WhitePlayer, new Position(0, 3)),
                PieceFactory.CreateQueen(WhitePlayer, new Position(7, 4)),
                PieceFactory.CreateQueen(WhitePlayer, new Position(2, 1)),
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateQueen(BlackPlayer, new Position(4, 6)),
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            MoveService.Move(whitePieces[0], new Position(3, 3), game);

            var isCheckmate = game.IsCheckmate();

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
                PieceFactory.CreatePawn(WhitePlayer, new Position(7, 5)),
                PieceFactory.CreateRook(WhitePlayer, new Position(5, 4)),
                PieceFactory.CreateKing(WhitePlayer, new Position(7, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(BlackPlayer, new Position(7, 6)),
                PieceFactory.CreateKing(BlackPlayer, new Position(7, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            MoveService.Move(whitePieces[1], new Position(6, 4), game);

            Assert.AreEqual(GameStatus.Stalemate, game.GameStatus.Value);
        }
    }
}
