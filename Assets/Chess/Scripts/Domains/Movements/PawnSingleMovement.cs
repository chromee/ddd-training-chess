using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements
{
    public class PawnSingleMovement : Movement
    {
        public PawnSingleMovement(MoveAmount moveAmount)
        {
            Movements = new[] { moveAmount, };
        }

        public override bool CanExecute(Game game, Piece piece, Position destination)
        {
            return !game.Board.ExistPiece(destination);
        }
    }
}
