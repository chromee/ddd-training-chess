using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public interface IPieceView
    {
        void SetPosition(Vector2 position);
        void Dead();
    }
}
