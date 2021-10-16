using Chess.Domain.Pieces;

namespace Chess.Domain.Moves.Conditions
{
    public class PawnSingleMoveCondition : IMoveConditions
    {
        public bool Conditions(Piece piece, Position destination, Board board)
        {
            return !board.ExistPiece(destination);
        }
    }
}
