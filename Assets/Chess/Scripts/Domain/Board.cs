using System.Linq;
using Chess.Domain.Pieces;

namespace Chess.Domain
{
    public class Board
    {
        public Piece[] Pieces { get; }

        public Board(Piece[] pieces)
        {
            Pieces = pieces;
        }

        public Piece GetPiece(Position position)
        {
            return Pieces.FirstOrDefault(v => v.Position == position);
        }

        public Piece GetPiece(PlayerColor playerColor, PieceType type)
        {
            return Pieces.FirstOrDefault(v => v.IsCorrectOwner(playerColor) && v.IsSameType(type));
        }

        public bool ExistPiece(Position position)
        {
            return GetPiece(position) != null;
        }

        public bool HasPiece(Piece piece) => Pieces.Contains(piece);
    }
}
