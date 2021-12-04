using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;
using UnityEngine;

namespace Chess.Scripts.Applications.Tests
{
    public class SelectPieceUseCaseTest : ApplicationTestBase
    {
        [Test]
        public void コマを選択できる()
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
                PieceFactory.CreateKing(PlayerColor.White, new Position(4, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());
            GameRegistry.Register(game);

            SelectPieceUseCase.TryExecute(new Vector2Int(4, 0));

            Assert.IsTrue(SelectedPieceRegistry.ExistSelectedPiece);
        }

        [Test]
        public void 敵のコマは選択できない()
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
                PieceFactory.CreateKing(PlayerColor.White, new Position(4, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());
            GameRegistry.Register(game);

            SelectPieceUseCase.TryExecute(new Vector2Int(4, 7));

            Assert.IsFalse(SelectedPieceRegistry.ExistSelectedPiece);
        }

        [Test]
        public void 死んだコマは選択できない()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePawn = PieceFactory.CreatePawn(PlayerColor.White, new Position(4, 1));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(PlayerColor.White, new Position(4, 0)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());
            GameRegistry.Register(game);

            whitePawn.Die();

            SelectPieceUseCase.TryExecute(new Vector2Int(4, 1));

            Assert.IsFalse(SelectedPieceRegistry.ExistSelectedPiece);
        }
    }
}
