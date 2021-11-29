using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;
using UnityEngine;

namespace Chess.Scripts.Applications.Tests
{
    public class PieceMoveUseCaseTest : ApplicationTestBase
    {
        [Test]
        public void コマを移動させられる()
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

            SelectPieceUseCase.SelectPiece(new Vector2Int(4, 0));
            PieceMoveUseCase.TryMovePiece(new Vector2Int(5, 0));

            Assert.AreEqual(new Position(5, 0), whiteKing.Position);
        }
    }
}
