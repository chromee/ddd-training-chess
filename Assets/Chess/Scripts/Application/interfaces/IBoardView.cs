using System;
using Chess.Domain;

namespace Chess.Application.interfaces
{
    public interface IBoardView
    {
        IObservable<Position> OnClicked { get; }
        void SetMovable(Position position);
        void ResetSquares();
    }
}
