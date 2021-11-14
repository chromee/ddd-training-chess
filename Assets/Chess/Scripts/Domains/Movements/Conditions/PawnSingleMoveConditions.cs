using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnSingleMoveConditions : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            return !board.ExistPiece(destination);
        }
    }
}
