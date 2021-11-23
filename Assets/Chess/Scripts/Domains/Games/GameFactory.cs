using System.Collections.Generic;
using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Games
{
    public class GameFactory
    {
        private readonly PieceFactory _pieceFactory;

        public GameFactory(PieceFactory pieceFactory)
        {
            _pieceFactory = pieceFactory;
        }

        public Game CreateGame()
        {
            var whitePieces = CreatePieces(PlayerColor.White);
            var blackPieces = CreatePieces(PlayerColor.Black);

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            return new Game(board);
        }

        private List<Piece> CreatePieces(PlayerColor color)
        {
            var pawnLine = color == PlayerColor.White ? PieceConstants.WhitePawnYLine : PieceConstants.BlackPawnYLine;
            var othersLine = color == PlayerColor.White ? PieceConstants.WhiteYLine : PieceConstants.BlackYLine;
            var pieces = new List<Piece>();
            for (var i = 0; i < 8; i++) pieces.Add(_pieceFactory.CreatePawn(color, new Position(i, pawnLine)));
            pieces.Add(_pieceFactory.CreateRook(color, new Position(0, othersLine)));
            pieces.Add(_pieceFactory.CreateKnight(color, new Position(1, othersLine)));
            pieces.Add(_pieceFactory.CreateBishop(color, new Position(2, othersLine)));
            pieces.Add(_pieceFactory.CreateQueen(color, new Position(PieceConstants.QueenX, othersLine)));
            pieces.Add(_pieceFactory.CreateKing(color, new Position(PieceConstants.KingX, othersLine)));
            pieces.Add(_pieceFactory.CreateBishop(color, new Position(5, othersLine)));
            pieces.Add(_pieceFactory.CreateKnight(color, new Position(6, othersLine)));
            pieces.Add(_pieceFactory.CreateRook(color, new Position(7, othersLine)));
            return pieces;
        }

        public Game CreateCheckmateGame()
        {
            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(PlayerColor.White, new Position(7, 6)),
                _pieceFactory.CreateKing(PlayerColor.White, new Position(3, 5)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            return new Game(board);
        }

        public Game CreateStalemateGame()
        {
            var whitePieces = new[]
            {
                _pieceFactory.CreateQueen(PlayerColor.White, new Position(7, 6)),
                _pieceFactory.CreateKing(PlayerColor.White, new Position(2, 7)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(PlayerColor.Black, new Position(0, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            return new Game(board);
        }

        public Game CreatePromotionGame()
        {
            var whitePieces = new[]
            {
                _pieceFactory.CreatePawn(PlayerColor.White, new Position(2, 6)),
                _pieceFactory.CreateKing(PlayerColor.White, new Position(4, 0)),
            };
            var blackPieces = new[]
            {
                _pieceFactory.CreateKing(PlayerColor.Black, new Position(4, 7)),
            };

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            return new Game(board);
        }
    }
}
