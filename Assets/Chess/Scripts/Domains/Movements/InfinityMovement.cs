using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements
{
    public class InfinityMovement : Movement
    {
        public InfinityMovement(MoveAmount direction)
        {
            Movements = new MoveAmount[7];
            for (var i = 0; i < 7; i++)
            {
                Movements[i] = direction * (i + 1);
            }
        }

        public override bool CanExecute(Game game, Piece piece, Position destination)
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
