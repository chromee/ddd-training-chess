using System;
using Chess.Scripts.Applications.Pieces;
using UnityEngine;

namespace Chess.Scripts.Presentations.Pieces
{
    [CreateAssetMenu(fileName = "PieceSpriteData", menuName = "PieceSpriteData", order = 0)]
    public class PieceSpriteData : ScriptableObject
    {
        [SerializeField] private Sprite _bishopSprite;
        [SerializeField] private Sprite _kingSprite;
        [SerializeField] private Sprite _knightSprite;
        [SerializeField] private Sprite _pawnSprite;
        [SerializeField] private Sprite _queenSprite;
        [SerializeField] private Sprite _rookSprite;

        public Sprite BishopSprite => _bishopSprite;
        public Sprite KingSprite => _kingSprite;
        public Sprite KnightSprite => _knightSprite;
        public Sprite PawnSprite => _pawnSprite;
        public Sprite QueenSprite => _queenSprite;
        public Sprite RookSprite => _rookSprite;

        public Sprite GetSprite(PieceType type)
        {
            switch (type)
            {
                case PieceType.Bishop:
                    return BishopSprite;
                case PieceType.King:
                    return KingSprite;
                case PieceType.Knight:
                    return KnightSprite;
                case PieceType.Pawn:
                    return PawnSprite;
                case PieceType.Queen:
                    return QueenSprite;
                case PieceType.Rook:
                    return RookSprite;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
