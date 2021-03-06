using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Chess.Scripts.Presentations.Boards
{
    public class BoardSquare : MonoBehaviour
    {
        [SerializeField] private Color _whiteColor;
        [SerializeField] private Color _blackColor;
        [SerializeField] private Color _movableColor;
        private Color _defaultColor;

        private SpriteRenderer _spriteRenderer;

        public Vector2Int Position { get; private set; }
        public bool IsMovable { get; private set; }

        public event UnityAction<Vector2Int> OnClicked;

        public void Initialize(Vector2Int position)
        {
            Position = position;

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _defaultColor = (Position.x + Position.y) % 2 == 0 ? _whiteColor : _blackColor;
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
