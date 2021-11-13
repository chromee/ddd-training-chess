using Chess.Scripts.Applications.Pieces;
using UnityEngine;

namespace Chess.Scripts.Presentations.Pieces
{
    public class PieceView : MonoBehaviour, IPieceView
    {
        [SerializeField] private PieceSpriteData _spriteData;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Color _whiteColor;
        [SerializeField] private Color _blackColor;

        public void Initialize(PieceData piece)
        {
            transform.position = new Vector3(piece.Position.x, piece.Position.y, 0);
            _spriteRenderer.sprite = _spriteData.GetSprite(piece.Type);
            _spriteRenderer.color = piece.Color == PieceColor.White ? _whiteColor : _blackColor;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, 0);
        }

        public void Dead()
        {
            _spriteRenderer.enabled = false;
        }

        public void Dispose()
        {
            if (gameObject != null) Destroy(gameObject);
        }
    }
}
