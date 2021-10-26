using System;
using UnityEngine;

namespace Chess.Application.interfaces
{
    public interface IBoardView
    {
        IObservable<Vector2> OnClicked { get; }
        void SetMovable(Vector2 position);
        void ResetSquares();
    }
}
