using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnSingleMoveCondition : IMoveCondition
    {
        public bool CanExecute(Game game, Piece piece, Position destination)
        {
            return !game.Board.ExistPiece(destination);
        }
    }
}
