using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Boards;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;

namespace Chess.Domain.Games
{
    public class GameFactory : IGameFactory
    {
        private readonly PieceFactory _pieceFactory;
        private readonly MoveService _moveService;

        public GameFactory(PieceFactory pieceFactory, MoveService moveService)
        {
            _pieceFactory = pieceFactory;
            _moveService = moveService;
        }

        public Game CreateGame()
        {
            var whitePlayer = new Player(PlayerColor.White);
            var blackPlayer = new Player(PlayerColor.Black);

            var whitePieces = CreatePieces(whitePlayer);
            var blackPieces = CreatePieces(blackPlayer);

            var board = new Board(whitePieces.Concat(blackPieces).ToList());
            return new Game(board, whitePlayer, blackPlayer, _moveService);
        }

        private List<Piece> CreatePieces(Player player)
        {
            var pawnLine = player.Color == PlayerColor.White ? 1 : 6;
            var othersLine = player.Color == PlayerColor.White ? 0 : 7;
            var pieces = new List<Piece>();
            for (var i = 0; i < 8; i++) pieces.Add(_pieceFactory.CreatePawn(player, new Position(i, pawnLine)));
            pieces.Add(_pieceFactory.CreateRook(player, new Position(0, othersLine)));
            pieces.Add(_pieceFactory.CreateKnight(player, new Position(1, othersLine)));
            pieces.Add(_pieceFactory.CreateBishop(player, new Position(2, othersLine)));
            pieces.Add(_pieceFactory.CreateQueen(player, new Position(3, othersLine)));
            pieces.Add(_pieceFactory.CreateKing(player, new Position(4, othersLine)));
            pieces.Add(_pieceFactory.CreateBishop(player, new Position(5, othersLine)));
            pieces.Add(_pieceFactory.CreateKnight(player, new Position(6, othersLine)));
            pieces.Add(_pieceFactory.CreateRook(player, new Position(7, othersLine)));
            return pieces;
        }
    }
}
