using Chess.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Chess.View
{
    public class BoardSquare : MonoBehaviour
    {
        public Position Position { get; private set; }
        public bool IsMovable { get; private set; }

        private SpriteRenderer _spriteRenderer;

        public event UnityAction<Position> OnClicked;

        public void Initialize(Position position)
        {
            Position = position;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            GetComponentInChildren<TMP_Text>().text = Position.ToString();
        }

        public void OnClick()
        {
            OnClicked?.Invoke(Position);
        }

        public void SetDefault()
        {
            _spriteRenderer.color = Color.white;
            IsMovable = false;
        }

        public void SetMovable()
        {
            _spriteRenderer.color = Color.red;
            IsMovable = true;
        }
    }
}
