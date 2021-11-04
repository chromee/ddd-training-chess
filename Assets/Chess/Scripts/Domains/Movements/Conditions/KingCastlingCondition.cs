using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class KingCastlingCondition : IMoveConditions
    {
        public bool CanExecute(Piece piece, Position destination, Board board)
        {
            if (!piece.IsType(PieceType.King)) return false;

            var isWhite = piece.Color == PlayerColor.White;
            var y = isWhite ? PieceConstants.WhiteYLine : PieceConstants.BlackYLine;

            // キャスリングは初期位置でなければ発動しない
            if (piece.Position.X != PieceConstants.KingX || piece.Position.Y != y) return false;

            // キャスリングは初期位置yラインでなければ発動しない
            if (destination.Y != y) return false;

            // 移動方向にルークがなければ発動しない
            var isRightMove = destination.X - piece.Position.X > 0;
            var rook = board.GetPiece(new Position(isRightMove ? 7 : 0, y));
            if (rook == null || rook.IsOpponent(piece) || !rook.IsType(PieceType.Rook)) return false;

            // ルークとの間にコマがあると発動しない
            if (isRightMove)
            {
                for (var i = piece.Position.X + 1; i < 7; i++)
                {
                    if (board.GetPiece(new Position(i, y)) != null) return false;
                }
            }
            else
            {
                for (var i = piece.Position.X - 1; i > 0; i--)
                {
                    if (board.GetPiece(new Position(i, y)) != null) return false;
                }
            }

            // 移動した結果、チェックになる場合は発動しない
            var cloneBoard = board.Clone();
            var cloneKing = cloneBoard.GetPiece(piece.Position);
            cloneBoard.MovePiece(cloneKing.Position, destination);
            if (cloneBoard.CanPick(cloneKing)) return false;

            return true;
        }
    }
}
