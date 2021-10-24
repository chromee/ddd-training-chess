using Chess.Domain.Boards;
using Chess.Domain.Pieces;

namespace Chess.Domain.Movements.Conditions
{
    public class PawnDoubleMoveCondition : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            // TODO: 流石にやばすぎ実装なのでなんとかする
            var initialY = piece.Color == PlayerColor.White ? 1 : 6;
            var isFirstMove = piece.Position.Y == initialY;
            return isFirstMove && !board.ExistPiece(destination);
        }
    }
}
