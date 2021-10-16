using Chess.Domain;
using Chess.Domain.Pieces;
using UnityEngine;
using UnityEngine.Events;

namespace Chess.View
{
    public class PieceBehavior : MonoBehaviour
    {
        [SerializeField] private PieceSpriteData _spriteData;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Piece _piece;

        public event UnityAction OnClicked;

        public void Initialize(Piece piece, Board board)
        {
            _piece = piece;
            transform.position = new Vector3(piece.Position.X, piece.Position.Y, 0);
            _spriteRenderer.sprite = _spriteData.GetSprite(_piece);
            _spriteRenderer.color = piece.Color == PlayerColor.White ? Color.white : Color.black;
            OnClicked += () =>
            {
                foreach (var pos in piece.MoveCandidates(board))
                {
                    Debug.LogError($"{piece}: {pos.ToString()}");
                }
            };
        }

        public void OnClick() => OnClicked?.Invoke();
    }
}
