using System;
using UnityEngine;

namespace Chess.Scripts.Applications.Boards
{
    public interface IBoardView : IDisposable
    {
        IObservable<Vector2Int> OnClicked { get; }
        void SetMovable(Vector2Int position);
        void ResetSquares();
    }
}
