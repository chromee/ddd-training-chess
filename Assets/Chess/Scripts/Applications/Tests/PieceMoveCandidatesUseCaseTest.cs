using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;
using UnityEngine;

namespace Chess.Scripts.Applications.Tests
{
    public class PieceMoveCandidatesUseCaseTest : TestBase
    {
        [Test]
        public void 正しい移動可能範囲が返ってくる()
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

            var destinations = PieceMoveCandidatesUseCase.Get(whiteKing);

            var correctDestinations = new[]
            {
                new Vector2Int(3, 0),
                new Vector2Int(3, 1),
                new Vector2Int(4, 1),
                new Vector2Int(5, 1),
                new Vector2Int(5, 0),
            };
            Assert.That(correctDestinations, Is.EquivalentTo(destinations));
        }
    }
}
