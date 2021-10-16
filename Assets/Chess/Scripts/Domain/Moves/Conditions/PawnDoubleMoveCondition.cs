using Chess.Domain.Pieces;

namespace Chess.Domain.Moves.Conditions
{
    public class PawnDoubleMoveCondition : IMoveConditions
    {
        public bool Conditions(Piece piece, Position destination, Board board)
        {
            // TODO: 流石にやばすぎ実装なのでなんとか綺麗にする
            var initialY = piece.Color == PlayerColor.White ? 1 : 6;
            var isFirstMove = piece.Position.Y == initialY;
            return isFirstMove && !board.ExistPiece(destination);
        }
    }
}
