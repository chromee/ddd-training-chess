using System.Collections.Generic;
using System.Linq;
using Chess.Application.interfaces;
using UnityEngine;

namespace Chess.Application.Services
{
    public class PiecesRegistry
    {
        private readonly List<IPieceView> _pieces = new List<IPieceView>();
        public void AddPiece(IPieceView pieceView) => _pieces.Add(pieceView);
        public void RemovePiece(IPieceView pieceView) => _pieces.Remove(pieceView);
        public IReadOnlyList<IPieceView> Pieces => _pieces;
        public IPieceView GetPiece(Vector2 position) => _pieces.FirstOrDefault(v => v.Position == position);
    }
}
