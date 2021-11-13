using System;
using UnityEngine;

namespace Chess.Scripts.Applications.Boards
{
    public interface IBoardView : IDisposable
    {
        IObservable<Vector2> OnClicked { get; }
        void SetMovable(Vector2 position);
        void ResetSquares();
    }
}
