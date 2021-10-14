using System.Collections.Generic;
using Chess.Domain.Entities.Pieces;
using Chess.Domain.Service;
using Chess.Domain.ValueObjects;

namespace Chess.Domain.Entities
{
    public sealed class Board
    {
        private readonly List<Piece> _pieces = new List<Piece>();
        public IReadOnlyList<Piece> Pieces => _pieces;

        public void AddPiece(Piece piece) => _pieces.Add(piece);
    }
}
