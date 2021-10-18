using Chess.Domain.Pieces;

namespace Chess.Domain.Movements.Conditions
{
    public interface IMoveConditions
    {
        bool CanExecute(Piece piece, Position destination, Board board);
    }
}
