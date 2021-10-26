using Chess.Application.Dto;
using Chess.Application.interfaces;
using UnityEngine;

namespace Chess.View.Views
{
    public class PieceView : MonoBehaviour, IPieceView
    {
        [SerializeField] private PieceSpriteData _spriteData;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Color _whiteColor;
        [SerializeField] private Color _blackColor;

        public Vector2 Position { get; set; }

        public void Initialize(PieceData piece)
        {
            transform.position = new Vector3(piece.Position.x, piece.Position.y, 0);
            _spriteRenderer.sprite = _spriteData.GetSprite(piece.Type);
            _spriteRenderer.color = piece.Color == PieceColor.White ? _whiteColor : _blackColor;
        }

        // TODO
        public void SetPosition(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, 0);
        }

        // TODO
        public void Dead()
        {
            _spriteRenderer.enabled = false;
        }
    }
}
