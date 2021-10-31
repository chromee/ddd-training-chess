using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public interface IMoveConditions
    {
        bool CanExecute(Piece piece, Position destination, Board board);
    }
}
