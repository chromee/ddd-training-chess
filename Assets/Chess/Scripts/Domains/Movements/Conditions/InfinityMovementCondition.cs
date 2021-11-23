using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class InfinityMovementCondition : IMovementCondition
    {
        public bool CanExecute(Game game, Piece piece, Position destination)
        {
            var destPiece = game.Board.GetPiece(destination);
            if (destPiece != null && !destPiece.IsOpponent(piece)) return false;

            var diff = destination - piece.Position;
            var dir = diff.Normalized();

            for (var i = 1;; i++)
            {
                var onTheWay = piece.Position + dir * i;
                if (onTheWay == destination) break;
                if (game.Board.ExistPiece(onTheWay)) return false;
            }

            return true;
        }
    }
}
