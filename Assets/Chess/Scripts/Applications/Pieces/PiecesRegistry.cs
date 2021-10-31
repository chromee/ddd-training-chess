using System.Collections.Generic;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Applications.Pieces
{
    public class PiecesRegistry
    {
        private readonly List<PiecePresenter> _pieces = new();
        public void AddPiece(PiecePresenter piece) => _pieces.Add(piece);
        public IReadOnlyList<PiecePresenter> Pieces => _pieces;
    }
}
