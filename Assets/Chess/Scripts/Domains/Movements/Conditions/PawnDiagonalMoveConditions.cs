using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnDiagonalMoveConditions : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            var destPiece = board.GetPiece(destination);
            return destPiece != null && destPiece.IsOpponent(piece);
        }
    }
}
