using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;
using UnityEngine;

namespace Chess.Scripts.Applications.Tests
{
    public class BoardUseCaseTest : TestBase
    {
        [Test]
        public void コマ選択と移動先選択ができる()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whiteKing = PieceFactory.CreateKing(PlayerColor.White, new Position(4, 0));
            var whitePieces = new[]
            {
                whiteKing,
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());
            GameRegistry.Register(game);

            BoardUseCase.SelectBoardSquare(new Vector2Int(4, 0));

            Assert.IsTrue(SelectedPieceRegistry.ExistSelectedPiece);

            BoardUseCase.SelectBoardSquare(new Vector2Int(5, 0));

            Assert.AreEqual(new Position(5, 0), whiteKing.Position);
        }
    }
}
