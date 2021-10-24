using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Pieces;

namespace Chess.Domain.Boards
{
    public class Board
    {
        public List<Piece> Pieces { get; }

        public Board(List<Piece> pieces)
        {
            Pieces = pieces;
        }

        public Piece GetPiece(Position position) => Pieces.FirstOrDefault(v => v.Position == position);

        public Piece GetPiece(Player player, PieceType type)
            => Pieces.FirstOrDefault(v => v.IsOwner(player) && v.IsSameType(type));

        public Piece[] GetPieces(Player player) => Pieces.Where(v => v.IsOwner(player)).ToArray();

        public bool ExistPiece(Position position) => GetPiece(position) != null;
        public bool HasPiece(Piece piece) => Pieces.Contains(piece);

        public void RemovePiece(Piece piece)
        {
            if (Pieces.Contains(piece)) Pieces.Remove(piece);
        }

        public Board Clone()
        {
            var pieces = new List<Piece>();
            foreach (var piece in Pieces)
            {
                pieces.Add(piece.Clone());
            }

            // var pieces = Pieces.Select(piece => piece.Clone()).ToList();
            return new Board(pieces);
        }
    }
}
