using Chess.Application.interfaces;
using Chess.Domain.Pieces;
using UnityEngine;

namespace Chess.View.Factories
{
    public class PieceViewFactory : IPieceViewFactory
    {
        private readonly ChessViewPrefabData _chessViewPrefabData;

        public PieceViewFactory(ChessViewPrefabData chessViewPrefabData)
        {
            _chessViewPrefabData = chessViewPrefabData;
        }

        public void CreatePieceView(Piece piece)
        {
            var pieceView = Object.Instantiate(_chessViewPrefabData.PieceViewPrefab);
            pieceView.Initialize(piece);
        }
    }
}
