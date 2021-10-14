using System;
using Chess.Domain.ValueObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chess.UI
{
    public class BoardSquare : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private Vector2 _coordinate;
        public UnityAction<Vector2> OnClicked;

        public void Initialize(Vector2 coordinate)
        {
            _coordinate = coordinate;
            _button.onClick.AddListener(() => OnClicked?.Invoke(_coordinate));
        }

        public void SetMovable(bool movable)
        {
        }

        public Vector2 GetSize()
        {
            return GetComponent<SpriteRenderer>().size;
        }
    }
}
