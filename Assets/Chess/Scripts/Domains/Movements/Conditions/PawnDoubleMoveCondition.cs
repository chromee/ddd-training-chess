using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnDoubleMoveCondition : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            // TODO: 流石にやばすぎ実装なのでなんとかする
            var initialY = piece.Color == PlayerColor.White ? 1 : 6;
            var blockY = piece.Color == PlayerColor.White ? 2 : 5;
            var isFirstMove = piece.Position.Y == initialY;
            return isFirstMove && !board.ExistPiece(destination) && !board.ExistPiece(new Position(destination.X, blockY));
        }
    }
}
