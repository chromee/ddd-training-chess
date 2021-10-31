using Chess.Scripts.Presentations.Boards;
using Chess.Scripts.Presentations.Pieces;
using UnityEngine;

namespace Chess.Scripts.Presentations
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
