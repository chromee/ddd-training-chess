using System;
using Chess.Domain;
using Chess.Domain.Boards;

namespace Chess.Application.interfaces
{
    public interface IBoardView
    {
        IObservable<Position> OnClicked { get; }
        void SetMovable(Position position);
        void ResetSquares();
    }
}
