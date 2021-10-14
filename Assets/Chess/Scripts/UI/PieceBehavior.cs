using Chess.Domain.Entities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chess.UI
{
    public class PieceBehavior : MonoBehaviour
    {
        [SerializeField] private PieceSpriteData _spriteData;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Button _button;

        private Piece _piece;

        public UnityAction OnClicked;

        private void Initialize(Piece piece)
        {
            _piece = piece;
            _spriteRenderer.sprite = _spriteData.GetSprite(_piece);
            _button.onClick.AddListener(() => OnClicked?.Invoke());
        }
    }
}
