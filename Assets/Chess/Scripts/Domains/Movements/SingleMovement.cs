using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements
{
    public class SingleMovement : Movement
    {
        public SingleMovement(MoveAmount moveAmount)
        {
            Movements = new[] { moveAmount, };
        }

        public override bool CanExecute(Game game, Piece piece, Position destination)
        {
            var destPiece = game.Board.GetPiece(destination);
            return destPiece == null || destPiece.IsOpponent(piece);
        }
    }
}
