using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnDoubleMoveConditions : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            // 目的地にコマがあると移動できない
            if (board.ExistPiece(destination)) return false;

            // 初期位置でなければ移動できない
            var initialY = piece.Color == PlayerColor.White ? 1 : 6;
            if (piece.Position.Y != initialY) return false;

            // 間にコマがあると移動できない
            var blockY = piece.Color == PlayerColor.White ? 2 : 5;
            if (board.ExistPiece(new Position(destination.X, blockY))) return false;

            return true;
        }
    }
}
