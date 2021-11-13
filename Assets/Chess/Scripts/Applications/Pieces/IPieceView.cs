using System;
using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public interface IPieceView : IDisposable
    {
        void SetPosition(Vector2 position);
        void Dead();
    }
}
