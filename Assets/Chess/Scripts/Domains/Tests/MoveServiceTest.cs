using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class MoveServiceTest : DomainTestBase
    {
        [Test]
        public void コマは2回連続動かせない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
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

            MoveService.Move(whitePieces[0], new Position(3, 4), game);

            Assert.Throws<WrongPlayerException>(() =>
                MoveService.Move(whitePieces[0], new Position(3, 3), game));
        }

        [Test]
        public void とられたコマは動かせない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
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

            MoveService.Move(whitePieces[0], new Position(3, 4), game);

            Assert.Throws<PieceNotExistOnBoardException>(() =>
                MoveService.Move(blackPieces[0], new Position(3, 5), game));
        }

        [Test]
        public void コマの行動範囲外には移動できない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            Assert.Throws<OutOfRangePieceMovableRangeException>(() =>
                MoveService.Move(whitePieces[0], new Position(3, 2), game));
        }

        [Test]
        public void 味方コマがいる位置には移動できない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreatePawn(WhitePlayer, new Position(3, 1)),
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            Assert.Throws<OutOfRangePieceMovableRangeException>(() =>
                MoveService.Move(whitePieces[1], new Position(3, 1), game));
        }

        [Test]
        public void 自殺行動はできない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ ○ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePieces = new[]
            {
                PieceFactory.CreateKing(WhitePlayer, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreatePawn(BlackPlayer, new Position(4, 2)),
                PieceFactory.CreateKing(BlackPlayer, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            var game = new Game(board, WhitePlayer, BlackPlayer, SpecialRules);

            Assert.Throws<SuicideMoveException>(() =>
                MoveService.Move(whitePieces[0], new Position(3, 1), game));
        }
    }
}
