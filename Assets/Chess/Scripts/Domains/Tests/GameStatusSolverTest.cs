using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;

namespace Chess.Scripts.Domains.Tests
{
    public class GameStatusSolverTest : DomainTestBase
    {
        [Test]
        public void チェック()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
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

            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());
            Assert.IsTrue(game.StatusSolver.IsCheck(game.CurrentTurnPlayer));
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


            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var isCheck = game.StatusSolver.IsCheck(game.CurrentTurnPlayer);

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


            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var isCheckmate = game.StatusSolver.IsCheckmate(game.CurrentTurnPlayer);

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


            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            PieceMovementExecutor.Move(game, whitePieces[0], new Position(3, 4));

            var isCheckmate = game.StatusSolver.IsCheckmate(game.CurrentTurnPlayer);

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


            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            PieceMovementExecutor.Move(game, whitePieces[0], new Position(3, 4));

            var isCheckmate = game.StatusSolver.IsCheckmate(game.CurrentTurnPlayer);

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


            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            PieceMovementExecutor.Move(game, whitePieces[0], new Position(3, 3));

            var isCheckmate = game.StatusSolver.IsCheckmate(game.CurrentTurnPlayer);

            Assert.IsFalse(isCheckmate);
        }

        [Test]
        public void スティルメイトによる引き分け()
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


            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            PieceMovementExecutor.Move(game, whitePieces[1], new Position(6, 4));

            Assert.AreEqual(GameStatus.Stalemate, game.CurrentStatus);
        }

        [Test]
        public void 両方キングのみになったら引き分け()
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
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());
            Assert.IsTrue(game.StatusSolver.IsDraw());
        }

        [Test]
        public void 五十手の間駒の取り合いが起こらなかったら引き分け()
        {
            // ☆ ○ ○ ○ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ● ● ● ★

            var whitePieces = new[]
            {
                PieceFactory.CreateRook(PlayerColor.White, new Position(3, 0)),
                PieceFactory.CreateRook(PlayerColor.White, new Position(2, 0)),
                PieceFactory.CreateRook(PlayerColor.White, new Position(1, 0)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(0, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateRook(PlayerColor.Black, new Position(4, 7)),
                PieceFactory.CreateRook(PlayerColor.Black, new Position(5, 7)),
                PieceFactory.CreateRook(PlayerColor.Black, new Position(6, 7)),
                PieceFactory.CreateKing(PlayerColor.Black, new Position(7, 7)),
            };

            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            ShuttleRun(whitePieces[0], blackPieces[0]);
            ShuttleRun(whitePieces[1], blackPieces[1], 21);

            Assert.IsFalse(game.StatusSolver.IsDraw(), "49手の間駒の取り合いが起こらなくてもまだ引き分けじゃない");

            PieceMovementExecutor.Move(game, blackPieces[3], new Position(7, 6));

            Assert.IsTrue(game.StatusSolver.IsDraw(), "50手の間駒の取り合いが起こらなかったら引き分け");

            void ShuttleRun(Piece whitePiece, Piece blackPiece, int stopCount = int.MaxValue)
            {
                var c = 0;
                for (var i = 1; i <= 7; i++)
                {
                    PieceMovementExecutor.Move(game, whitePiece, new Position(whitePiece.Position.X, i));
                    c++;
                    if (c >= stopCount) return;
                    PieceMovementExecutor.Move(game, blackPiece, new Position(blackPiece.Position.X, 7 - i));
                    c++;
                    if (c >= stopCount) return;
                }

                for (var i = 1; i <= 7; i++)
                {
                    PieceMovementExecutor.Move(game, whitePiece, new Position(whitePiece.Position.X, 7 - i));
                    c++;
                    if (c >= stopCount) return;
                    PieceMovementExecutor.Move(game, blackPiece, new Position(blackPiece.Position.X, i));
                    c++;
                    if (c >= stopCount) return;
                }
            }
        }

        [Test]
        public void 三回同じ盤面になったら引き分け()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ ●

            var whitePieces = new[]
            {
                PieceFactory.CreateQueen(PlayerColor.White, new Position(0, 0)),
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            for (var i = 0; i < 2; i++)
            {
                PieceMovementExecutor.Move(game, whitePieces[1], new Position(4, 0));
                PieceMovementExecutor.Move(game, blackPieces[0], new Position(4, 7));
                PieceMovementExecutor.Move(game, whitePieces[1], new Position(3, 0));
                PieceMovementExecutor.Move(game, blackPieces[0], new Position(3, 7));
            }

            Assert.IsFalse(game.StatusSolver.IsDraw(), "２回目の重複盤面では引き分けにならない");

            PieceMovementExecutor.Move(game, whitePieces[1], new Position(4, 0));

            Assert.IsTrue(game.StatusSolver.IsDraw(), "３回同じ盤面になったら引き分け");
        }
    }
}
