using Chess.Application.interfaces;
using Chess.Domain;
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

        public IBoardView CreateBoardView(Board board)
        {
            return Object.Instantiate(_chessViewPrefabData.BoardViewPrefab);
        }
    }
}
