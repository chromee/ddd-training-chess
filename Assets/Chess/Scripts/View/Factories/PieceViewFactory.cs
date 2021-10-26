using Chess.Application.Dto;
using Chess.Application.interfaces;
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

        public IPieceView CreatePieceView(PieceData pieceData)
        {
            var pieceView = Object.Instantiate(_chessViewPrefabData.PieceViewPrefab);
            pieceView.Initialize(pieceData);
            return pieceView;
        }
    }
}
