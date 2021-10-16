using Chess.Domain.Pieces;

namespace Chess.Domain.Moves.Conditions
{
    public class InfinityMoveConditions : IMoveConditions
    {
        public bool Conditions(Piece piece, Position destination, Board board)
        {
            var destPiece = board.GetPiece(destination);
            if (piece.IsCorrectOwner(piece.Color)) return false;

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
