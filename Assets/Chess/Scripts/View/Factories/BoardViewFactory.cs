using Chess.Application.interfaces;
using UnityEngine;

namespace Chess.View.Factories
{
    public class BoardViewFactory : IBoardViewFactory
    {
        private readonly ChessViewPrefabData _chessViewPrefabData;

        public BoardViewFactory(ChessViewPrefabData chessViewPrefabData)
        {
            _chessViewPrefabData = chessViewPrefabData;
        }

        public IBoardView CreateBoardView()
        {
            return Object.Instantiate(_chessViewPrefabData.BoardViewPrefab);
        }
    }
}
