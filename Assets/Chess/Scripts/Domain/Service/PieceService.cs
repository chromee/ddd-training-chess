using Chess.Domain.Entities;
using Chess.Domain.Entities.Pieces;
using Chess.Domain.ValueObjects;

namespace Chess.Domain.Service
{
    public class PieceService
    {
        public static void GeneratePiece(Board board, PlayerColor color)
        {
            var pawnRow = color == PlayerColor.White ? 1 : 6;
            var othersRow = color == PlayerColor.White ? 0 : 7;

            for (var col = 0; col < 8; col++)
            {
                board.AddPiece(new Pawn(color, new Coordinate(col, pawnRow)));
            }

            board.AddPiece(new Bishop(color, new Coordinate(0, othersRow)));
            board.AddPiece(new Bishop(color, new Coordinate(7, othersRow)));

            board.AddPiece(new Knight(color, new Coordinate(1, othersRow)));
            board.AddPiece(new Knight(color, new Coordinate(6, othersRow)));

            board.AddPiece(new Rook(color, new Coordinate(2, othersRow)));
            board.AddPiece(new Rook(color, new Coordinate(5, othersRow)));

            board.AddPiece(new Queen(color, new Coordinate(3, othersRow)));

            board.AddPiece(new King(color, new Coordinate(4, othersRow)));
        }
    }
}
