using System.Linq;
using Chess.Domain.Pieces;

namespace Chess.Domain.Games
{
    public class GameFactory : IGameFactory
    {
        private readonly PieceFactory _pieceFactory;

        public GameFactory()
        {
            _pieceFactory = new PieceFactory();
        }

        public Game CreateGame()
        {
            var whitePlayer = new Player(PlayerColor.White);
            var blackPlayer = new Player(PlayerColor.Black);

            var whitePieces = _pieceFactory.CreatePieces(whitePlayer);
            var blackPieces = _pieceFactory.CreatePieces(blackPlayer);

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            return new Game(board, whitePlayer, blackPlayer);
        }
    }
}
