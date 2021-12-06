using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements
{
    public class PawnDoubleMovement : Movement
    {
        public PawnDoubleMovement(MoveAmount moveAmount)
        {
            Movements = new[] { moveAmount, };
        }

        public override bool CanExecute(Game game, Piece piece, Position destination)
        {
            // 目的地にコマがあると移動できない
            if (game.Board.ExistPiece(destination)) return false;

            // 初期位置でなければ移動できない
            var initialY = piece.IsWhite() ? 1 : 6;
            if (piece.Position.Y != initialY) return false;

            // 間にコマがあると移動できない
            var blockY = piece.IsWhite() ? 2 : 5;
            if (game.Board.ExistPiece(new Position(destination.X, blockY))) return false;

            return true;
        }
    }
}
