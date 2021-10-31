using Chess.Scripts.Applications.Pieces;
using UnityEngine;

namespace Chess.Scripts.Presentations.Pieces
{
    public class PieceViewFactory : IPieceViewFactory
    {
        private readonly ChessViewPrefabData _chessViewPrefabData;

        public PieceViewFactory(ChessViewPrefabData chessViewPrefabData)
        {
            _chessViewPrefabData = chessViewPrefabData;
        }

        public IPieceView CreatePieceView(PieceData pieceData)
        {
            var pieceView = Object.Instantiate(_chessViewPrefabData.PieceViewPrefab);
            pieceView.Initialize(pieceData);
            return pieceView;
        }
    }
}
