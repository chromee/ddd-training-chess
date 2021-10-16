using Chess.Domain.Pieces;

namespace Chess.Domain.Moves.Conditions
{
    public class PawnDiagonalMoveCondition : IMoveConditions
    {
        public bool Conditions(Piece piece, Position destination, Board board)
        {
            return board.ExistPiece(destination);
        }
    }
}
