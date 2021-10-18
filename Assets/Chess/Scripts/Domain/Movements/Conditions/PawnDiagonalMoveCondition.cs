using Chess.Domain.Pieces;

namespace Chess.Domain.Movements.Conditions
{
    public class PawnDiagonalMoveCondition : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            var destPiece = board.GetPiece(destination);
            return destPiece != null && destPiece.IsOpponent(piece);
        }
    }
}
