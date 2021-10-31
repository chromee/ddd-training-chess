using Chess.Scripts.Applications.Boards;
using UnityEngine;

namespace Chess.Scripts.Presentations.Boards
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
