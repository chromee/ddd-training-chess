using Chess.Domain;
using Chess.Domain.Games;
using Chess.Domain.Pieces;
using UnityEngine;

namespace Chess.View.Views
{
    public class PieceView : MonoBehaviour
    {
        [SerializeField] private PieceSpriteData _spriteData;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Color _whiteColor;
        [SerializeField] private Color _blackColor;

        private Piece _piece;

        public void Initialize(Piece piece)
        {
            _piece = piece;
            transform.position = new Vector3(_piece.Position.X, _piece.Position.Y, 0);
            _spriteRenderer.sprite = _spriteData.GetSprite(_piece);
            _spriteRenderer.color = _piece.Color == PlayerColor.White ? _whiteColor : _blackColor;
        }

        public void Update()
        {
            if (_piece.Dead)
            {
                _spriteRenderer.enabled = false;
                return;
            }

            transform.position = new Vector3(_piece.Position.X, _piece.Position.Y, 0);
        }
    }
}
