using Chess.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Chess.View
{
    public class BoardSquare : MonoBehaviour
    {
        [SerializeField] private Color _whiteColor;
        [SerializeField] private Color _blackColor;
        [SerializeField] private Color _movableColor;

        public Position Position { get; private set; }
        public bool IsMovable { get; private set; }

        private SpriteRenderer _spriteRenderer;
        private Color _defaultColor;

        public event UnityAction<Position> OnClicked;

        public void Initialize(Position position)
        {
            Position = position;

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _defaultColor = (Position.X + Position.Y) % 2 == 0 ? _whiteColor : _blackColor;
            _spriteRenderer.color = _defaultColor;

            GetComponentInChildren<TMP_Text>().text = Position.ToString();
        }

        public void OnClick()
        {
            OnClicked?.Invoke(Position);
        }

        public void SetDefault()
        {
            _spriteRenderer.color = _defaultColor;
            IsMovable = false;
        }

        public void SetMovable()
        {
            _spriteRenderer.color = _movableColor;
            IsMovable = true;
        }
    }
}
