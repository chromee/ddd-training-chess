using System.Collections.Generic;
using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;

namespace Chess.Scripts.Domains.Games
{
    public class GameFactory : IGameFactory
    {
        private readonly PieceFactory _pieceFactory;

        public GameFactory(PieceFactory pieceFactory)
        {
            _pieceFactory = pieceFactory;
        }

        public Game CreateGame()
        {
            var whitePlayer = new Player(PlayerColor.White);
            var blackPlayer = new Player(PlayerColor.Black);

            var whitePieces = CreatePieces(whitePlayer);
            var blackPieces = CreatePieces(blackPlayer);

            var board = new Board(whitePieces.Concat(blackPieces).ToList());

            var specialRules = new SpecialRule[]
            {
                new EnPassant(),
                new Castling(),
                new Promotion(),
            };

            return new Game(board, whitePlayer, blackPlayer, specialRules);
        }

        private List<Piece> CreatePieces(Player player)
        {
            var pawnLine = player.Color == PlayerColor.White ? PieceConstants.WhitePawnYLine : PieceConstants.BlackPawnYLine;
            var othersLine = player.Color == PlayerColor.White ? PieceConstants.WhiteYLine : PieceConstants.BlackYLine;
            var pieces = new List<Piece>();
            for (var i = 0; i < 8; i++) pieces.Add(_pieceFactory.CreatePawn(player, new Position(i, pawnLine)));
            pieces.Add(_pieceFactory.CreateRook(player, new Position(0, othersLine)));
            pieces.Add(_pieceFactory.CreateKnight(player, new Position(1, othersLine)));
            pieces.Add(_pieceFactory.CreateBishop(player, new Position(2, othersLine)));
            pieces.Add(_pieceFactory.CreateQueen(player, new Position(PieceConstants.QueenX, othersLine)));
            pieces.Add(_pieceFactory.CreateKing(player, new Position(PieceConstants.KingX, othersLine)));
            pieces.Add(_pieceFactory.CreateBishop(player, new Position(5, othersLine)));
            pieces.Add(_pieceFactory.CreateKnight(player, new Position(6, othersLine)));
            pieces.Add(_pieceFactory.CreateRook(player, new Position(7, othersLine)));
            return pieces;
        }
    }
}
