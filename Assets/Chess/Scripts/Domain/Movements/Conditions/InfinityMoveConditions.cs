using Chess.Domain.Pieces;

namespace Chess.Domain.Movements.Conditions
{
    public class InfinityMoveConditions : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            var destPiece = board.GetPiece(destination);
            if (destPiece != null && !destPiece.IsOpponent(piece)) return false;

            var diff = destination - piece.Position;
            var dir = diff.Normalized();

            for (var i = 1;; i++)
            {
                var onTheWay = piece.Position + dir * i;
                if (onTheWay == destination) break;
                if (board.ExistPiece(onTheWay)) return false;
            }

            return true;
        }
    }
}
