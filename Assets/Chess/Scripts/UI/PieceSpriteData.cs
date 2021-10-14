using System;
using Chess.Domain.Entities;
using Chess.Domain.Entities.Pieces;
using UnityEngine;

namespace Chess.UI
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

        public Sprite GetSprite(Piece piece)
        {
            switch (piece)
            {
                case Bishop:
                    return BishopSprite;
                case King:
                    return KingSprite;
                case Knight:
                    return KnightSprite;
                case Pawn:
                    return PawnSprite;
                case Queen:
                    return QueenSprite;
                case Rook:
                    return RookSprite;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
