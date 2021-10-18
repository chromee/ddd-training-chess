using Chess.View.Views;
using UnityEngine;

namespace Chess.View.Factories
{
    [CreateAssetMenu(fileName = "ChessViewPrefabData", menuName = "Chess/ChessViewPrefabData", order = 0)]
    public class ChessViewPrefabData : ScriptableObject
    {
        [SerializeField] private BoardView _boardViewPrefab;
        [SerializeField] private PieceView _pieceViewPrefab;

        public BoardView BoardViewPrefab => _boardViewPrefab;
        public PieceView PieceViewPrefab => _pieceViewPrefab;
    }
}
