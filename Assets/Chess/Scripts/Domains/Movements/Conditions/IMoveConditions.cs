using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public interface IMoveConditions
    {
        bool CanExecute(Game game, Piece piece, Position destination);
    }
}
